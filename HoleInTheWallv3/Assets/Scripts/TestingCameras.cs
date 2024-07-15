using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingCameras : MonoBehaviour
{
    public RawImage cameraImage;
    public RawImage XRcameraImage;
    private WebCamTexture webcamTexture;

    public RawImage avatarImage;
    public RawImage XRavatarImage;
    public Camera avatarCamera;
    private RenderTexture renderTexture;

    public Canvas canvas;
    public Canvas XRcanvas;
    public GameObject XRcanvasFollow;
    private float distance = 3f;

    public bool showInXR = true;
    public bool cameraFollowAvatar = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get the default webcam and start playing
        webcamTexture = new WebCamTexture();
        cameraImage.texture = webcamTexture;
        cameraImage.material.mainTexture = webcamTexture;
        XRcameraImage.texture = webcamTexture;
        XRcameraImage.material.mainTexture = webcamTexture;
        webcamTexture.Play();

        //camera for showing the avatars movements
        renderTexture = new RenderTexture(Screen.width, Screen.height, 16);
        avatarCamera.targetTexture = renderTexture;
        avatarImage.texture = renderTexture;
        XRavatarImage.texture = renderTexture;

        //moving the images to see the camera images
        if (showInXR) XRcanvas.transform.position = XRcanvasFollow.transform.position + new Vector3(0, 0, distance);
        canvas.transform.position = new Vector3(14, 3, 3);
    }

    void OnDestroy()
    {
        // Stop the webcam when the object is destroyed
        webcamTexture.Stop();
    }

    private void FixedUpdate()
    {
        if (showInXR)
        {
            Vector3 inFrontOfCamera = XRcanvasFollow.transform.position + XRcanvasFollow.transform.forward * distance;
            //Update the object's position
            XRcanvas.transform.position = inFrontOfCamera;
            //make the object face the same direction as the camera
            XRcanvas.transform.rotation = XRcanvasFollow.transform.rotation;
        }
        //move the camera that is facing the image
        if(cameraFollowAvatar) avatarCamera.transform.position = XRcanvasFollow.transform.position + new Vector3(0, 0, 3.05f);
    }
}
