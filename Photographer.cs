using UnityEngine;

public class Photographer : MonoBehaviour {
  PhotoCapture photo = null;
  void Start() {
    PhotoCapture.CreateAsync(false, OnPhotoCaptureCreated);
  }

  void OnPhotoCaptureCreated(PhotoCapture capture) {
    photo = capture;

    Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();

    CameraParameters c = new CameraParameters();
    c.hologramOpacity = 0.0f;
    c.cameraResolutionWidth = cameraResolution.width;
    c.cameraResolutionHeight = cameraResolution.height;
    c.pixelFormat = CapturePixelFormat.BGRA32;

    captureObject.StartPhotoModeAsync(c, false, OnPhotoModeStarted);
  }

  void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result) {
       photoCaptureObject.Dispose();
       photoCaptureObject = null;
   }

  void OnPhotoModeStarted(PhotoCapture.PhotoCaptureResult result) {
    if (result.success) {
      photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
    } else {
      Debug.LogError("Unable to start photo mode!");
    }
  }

  void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame) {
    if (result.success) {
      Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
      Texture2D targetTexture = new Texture2D(cameraResolution.width, cameraResolution.height);
      photoCaptureFrame.UploadImageDataToTexture(targetTexture);

      // add to shape

    }
    photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
  }
}