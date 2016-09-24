using UnityEngine;

public class Photographer : MonoBehaviour {
  string FILENAME = "translationImage.jpg";
  PhotoCapture photo = null;

  // Create capture object
  void Start() {
    PhotoCapture.CreateAsync(false, OnPhotoCaptureCreated);
  }

  // Set up camera parameters, enter photo mode
  void OnPhotoCaptureCreated(PhotoCapture capture) {
    photo = capture;

    Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();

    CameraParameters cameraParams = new CameraParameters();
    cameraParams.hologramOpacity = 0.0f;
    cameraParams.cameraResolutionWidth = cameraResolution.width;
    cameraParams.cameraResolutionHeight = cameraResolution.height;
    cameraParams.pixelFormat = CapturePixelFormat.BGRA32;

    captureObject.StartPhotoModeAsync(c, false, OnPhotoModeStarted);
  }

  // Cleanup
  void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result) {
       photoCaptureObject.Dispose();
       photoCaptureObject = null;
   }

  // Take pic, save file 
  void OnPhotoModeStarted(PhotoCapture.PhotoCaptureResult result) {
    if (result.success) {
      string filepath = System.IO.Path.Combine(Application.persistentDataPath, FILENAME);
      photoCaptureObject.TakePhotoAsync(filepath, PhotoCaptureFileOutputFormat.JPG, OnCapturedPhotoToMemory);
    } else {
      Debug.LogError("Error starting photo mode.");
    }
  }

  // Cleanup on completion
  void OnCapturedPhotoToDisk(PhotoCapture.PhotoCaptureResult result) {
    if (result.success) {

      // http GET

      photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
    }
  }
}