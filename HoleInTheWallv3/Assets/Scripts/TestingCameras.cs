using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingCameras : MonoBehaviour
{
    public RawImage cameraImage;
    private WebCamTexture webcamTexture;

    public RawImage avatarImage;
    public Camera avatarCamera;
    private RenderTexture renderTexture;

    public Canvas canvas;
    public GameObject canvasFollow;
    private float rotateSpeed = 50f;
    private float distance = 3f;

    // Start is called before the first frame update
    void Start()
    {
        // Get the default webcam and start playing
        webcamTexture = new WebCamTexture();
        cameraImage.texture = webcamTexture;
        cameraImage.material.mainTexture = webcamTexture;
        webcamTexture.Play();

        renderTexture = new RenderTexture(Screen.width, Screen.height, 16);
        avatarCamera.targetTexture = renderTexture;
        avatarImage.texture = renderTexture;

        canvas.transform.position = canvasFollow.transform.position + new Vector3(0, 0, distance);
    }

    void OnDestroy()
    {
        // Stop the webcam when the object is destroyed
        webcamTexture.Stop();
    }

    private void FixedUpdate()
    {
        Vector3 inFrontOfCamera = canvasFollow.transform.position + canvasFollow.transform.forward * distance;

        // Update the object's position
        canvas.transform.position = inFrontOfCamera;

        // Optionally, make the object face the same direction as the camera
        canvas.transform.rotation = canvasFollow.transform.rotation;

        avatarCamera.transform.position = canvasFollow.transform.position + new Vector3(0, 0, 3.05f);
    }
}
