using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras; // Array to hold references to the cameras

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
        }
    }

    public void SwitchCamera(int cameraIndex)
    {
        // Deactivate all cameras
        foreach (Camera cam in cameras)
        {
            cam.gameObject.SetActive(false);
        }

        // Activate the specified camera
        if (cameraIndex >= 0 && cameraIndex < cameras.Length)
        {
            cameras[cameraIndex].gameObject.SetActive(true);
        }
    }
}

