using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace com.sumodh.lanstream.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoController : Controller
    {
        private readonly ILogger<VideoController> _logger;
        private const string VideoFilePath = "Content/SampleVideo.mp4";

        public VideoController(ILogger<VideoController> logger)
        {
            _logger = logger;
        }

        [HttpGet("stream")]
        public async Task<IActionResult> StreamVideo()
        {
            if (!System.IO.File.Exists(VideoFilePath))
            {
                _logger.LogError("Video file not found: {VideoFilePath}", VideoFilePath);
                return NotFound();
            }

            var fileInfo = new FileInfo(VideoFilePath);
            var fileStream = new FileStream(VideoFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var response = Response;
            response.Headers.Add("Accept-Ranges", "bytes");

            if (Request.Headers.ContainsKey("Range"))
            {
                var rangeHeader = Request.Headers["Range"].ToString();
                var range = rangeHeader.Replace("bytes=", "").Split('-');
                long start = long.Parse(range[0]);
                long end = range.Length > 1 && !string.IsNullOrEmpty(range[1]) ? long.Parse(range[1]) : fileInfo.Length - 1;
                long contentLength = end - start + 1;

                response.StatusCode = (int)HttpStatusCode.PartialContent;
                response.Headers.Add("Content-Range", $"bytes {start}-{end}/{fileInfo.Length}");
                response.ContentLength = contentLength;

                fileStream.Seek(start, SeekOrigin.Begin);
                return File(fileStream, "video/mp4", enableRangeProcessing: true);
            }

            response.ContentLength = fileInfo.Length;
            return File(fileStream, "video/mp4", enableRangeProcessing: true);
        }
    }
}
