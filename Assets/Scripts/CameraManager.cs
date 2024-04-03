using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera[] cameras;
    private int currentCameraIndex = 0;

    void Start()
    {
        // ensure that at least one camera is available
        if (cameras.Length == 0)
        {
            Debug.LogError("No cameras assigned to the CameraManager.");
            enabled = false;
            return;
        }

        // disable all cameras except the first one
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            SwitchToNextCamera();
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SwitchToPreviousCamera();
        }
    }

    void SwitchToNextCamera()
    {
        // Disable the current camera
        cameras[currentCameraIndex].gameObject.SetActive(false);

        // Move to the next camera index
        currentCameraIndex++;
        if (currentCameraIndex >= cameras.Length)
        {
            currentCameraIndex = 0; // Loop back to the first camera
        }

        // Enable the new current camera
        cameras[currentCameraIndex].gameObject.SetActive(true);
    }

    void SwitchToPreviousCamera()
    {
        // Disable the current camera
        cameras[currentCameraIndex].gameObject.SetActive(false);

        // Move to the previous camera index
        currentCameraIndex--;
        if (currentCameraIndex < 0)
        {
            currentCameraIndex = cameras.Length - 1; // Loop back to the last camera
        }

        // Enable the new current camera
        cameras[currentCameraIndex].gameObject.SetActive(true);
    }
}

