using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerMovement;
using Cinemachine;

// This script is a C# script for Unity that provides mouse look functionality for a player character.
// The script is attached to the main camera of the player and uses the UnityEngine input system to get the mouse input.
// The script uses the mouse X and Y axis to control the rotation of the camera and player body. It has a public float variable "mouseSensitivity" which can be adjusted in the Unity editor to change how sensitive the mouse input is.
// It also has a public Transform variable "playerBody" which is the transform component of the player's body.
// The script uses the "Update" function to constantly update the rotation of the camera and player body based on the mouse input.
// The "Start" function is used to lock the cursor to the center of the screen. Additionally, the script uses the Mathf.Clamp function to limit the x-rotation of the camera to a range of -90 to 90 degrees to prevent the camera from flipping upside down.

public class MouseLooks : MonoBehaviour
{
   
    public float mouseSensitivity = 200f;

    public Transform playerBody;

    float xRotation = 0f;
    [SerializeField]CinemachineVirtualCamera playerCam;
    [SerializeField][Range(0f, 2f)] float cameraHeight;
    [SerializeField][Range(0f, 2f)] float cameraHeightSneak;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.position = playerBody.position;

    }

    // Update is called once per frame
    void Update()
    {

        //This code is getting the input values for the mouse's X and Y axis.
        //The Input.GetAxis function is used to get the value of the axis, in this case "Mouse X" and "Mouse Y".
        //The input values are then multiplied by the "mouseSensitivity" variable and Time.deltaTime.
        //This is done to adjust the sensitivity of the mouse input and to make the rotation smooth.
        //Time.deltaTime is the time in seconds it took to complete the last frame, this value is used to make the movement frame rate independent.
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // This code is adjusting the rotation of the camera based on the mouseY input.
        // The current xRotation value is decreased by the mouseY input, this will make the camera rotate up when the mouse is moved up and rotate down when the mouse is moved down.
        // Then the Mathf.Clamp function is used to limit the xRotation value to a range of -90 to 90 degrees, this is to prevent the camera from flipping upside down.
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        if (PlayerMovement.sneak)
        {
            //transform.localPosition = new Vector3(0, -0.01f, 0);
            playerCam.transform.localPosition = new Vector3(0, -cameraHeightSneak, 0);

        }
        else
        {
            //transform.localPosition = new Vector3(0, 1.6f, 0);
            playerCam.transform.localPosition = new Vector3(0, cameraHeight, 0);
        }

        // This code is used to apply the rotation to the camera and player body.
        // The first line of code is used to set the rotation of the camera's transform component to the xRotation value.
        // The Quaternion.Euler function is used to convert the euler angles to a quaternion. The second line of code is used to rotate the player body's transform component by the mouseX input.
        // The Rotate function is used to rotate the transform component by a certain amount, in this case it is rotated by the mouseX input along the up vector.

        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

    }
}
