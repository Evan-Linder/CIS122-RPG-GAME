using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; // Array to hold references to the cameras
    public Canvas canvas;    // Reference to the UI Canvas

    private void Start()
    {
        // Initially, deactivate all cameras and activate the first one (if you want)
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        if (cameras.Length > 0)
        {
            cameras[0].gameObject.SetActive(true); // Activate the first camera by default
            UpdateCanvasCamera(0); // Update the canvas to use the first camera
        }
    }

    public void SwitchCamera(int cameraIndex)
    {
        // Deactivate all cameras
        foreach (Camera cam in cameras)
        {
            cam.gameObject.SetActive(false);
        }

        // Activate the specified camera and update the canvas
        if (cameraIndex >= 0 && cameraIndex < cameras.Length)
        {
            cameras[cameraIndex].gameObject.SetActive(true);
            UpdateCanvasCamera(cameraIndex); // Update the canvas to follow the active camera
        }
    }

    private void UpdateCanvasCamera(int cameraIndex)
    {
        if (canvas != null && cameraIndex >= 0 && cameraIndex < cameras.Length)
        {
            canvas.worldCamera = cameras[cameraIndex];
        }
    }
}


