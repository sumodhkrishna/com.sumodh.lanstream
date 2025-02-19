# Video Streaming API

## Overview
This API provides an endpoint to stream a video file efficiently. It supports HTTP range requests, allowing for smooth video playback in browsers.

## Features
- Streams video files from the `/content` directory.
- Supports range requests for efficient playback.
- Logs errors if the video file is missing.
- Returns appropriate HTTP status codes.

## Requirements
- .NET 6 or later
- ASP.NET Core Web API
- Video file placed in `/Content/SampleVideo.mp4`

## Installation
1. Clone the repository:
   ```sh
   git clone https://github.com/sumodhkrishna/com.sumodh.lanstream.git
   cd com.sumodh.lanstream
   ```
2. Restore dependencies:
   ```sh
   dotnet restore
   ```
3. Build the project:
   ```sh
   dotnet build
   ```
4. Run the application:
   ```sh
   dotnet run
   ```

## API Endpoint
### Stream Video
#### Request
**GET** `/api/video/stream`

#### Headers
- `Range: bytes=0-` (Optional, enables partial content delivery)

#### Response
- **200 OK** (If no range request is present)
- **206 Partial Content** (If range request is used)
- **404 Not Found** (If the video file does not exist)

#### Example Usage
Using `curl`:
```sh
curl -i -H "Range: bytes=0-1024" http://localhost:5000/api/video/stream
```

## License
This project is open-source and licensed under the MIT License.

## Contributing
Feel free to fork this repository and contribute by submitting a pull request.

## Contact
For issues and inquiries, please open an issue on the GitHub repository.

