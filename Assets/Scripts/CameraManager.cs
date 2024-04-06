using UnityEngine;
using TMPro;

public class CameraManager : MonoBehaviour
{
    public Camera[] cameras;
    private int currentCameraIndex = 0;
    public TextMeshProUGUI camNoText;

    public AudioManager audioManager;

    void Start()
    {
        // Ensure that at least one camera is available
        if (cameras.Length == 0)
        {
            Debug.LogError("No cameras assigned to the CameraManager.");
            enabled = false;
            return;
        }

        // Disable all cameras except the first one
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        // Update the text initially
        UpdateCameraText();
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

    public void SwitchToNextCamera()
    {
        // Disable the current camera
        cameras[currentCameraIndex].gameObject.SetActive(false);

        // Move to the next camera index
        currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

        // Enable the new current camera
        cameras[currentCameraIndex].gameObject.SetActive(true);

        // Update the text
        UpdateCameraText();

        // Play click sound effect
        audioManager.clickSFX();
    }

    public void SwitchToPreviousCamera()
    {
        // Disable the current camera
        cameras[currentCameraIndex].gameObject.SetActive(false);

        // Move to the previous camera index
        currentCameraIndex = (currentCameraIndex - 1 + cameras.Length) % cameras.Length;

        // Enable the new current camera
        cameras[currentCameraIndex].gameObject.SetActive(true);

        // Update the text
        UpdateCameraText();

        // Play click sound effect
        audioManager.clickSFX();
    }

    void UpdateCameraText()
    {
        // Update the TextMeshProUGUI component with the current camera number
        camNoText.text = "Cam " + (currentCameraIndex + 1);
    }
}
