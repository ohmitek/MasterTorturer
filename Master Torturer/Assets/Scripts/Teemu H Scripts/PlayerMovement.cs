using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  This code is for a Unity script that controls the movement of a player character in a game.
  It uses the UnityEngine CharacterController component to move the player object and the Input.GetAxis() method to get input from the horizontal and vertical axes.
  The script uses the horizontal input to move the player object right and the vertical input to move it forward.
  The speed variable is used to control the movement speed of the player, and the Time.deltaTime is used to ensure smooth movement.
  The Update() method is called once per frame, so the player's movement will be updated every frame based on the input received.
*/

public class PlayerMovement : MonoBehaviour
{
    //This class has a public CharacterController variable named controller, which will be used to control the movement of the player object.

    public CharacterController controller;

    //The class also has a public float variable named speed, which is used to control the speed of the player's movement.

    public float speed = 12f;

    //public boolean variable named "sneak" and check its value in the Update() method before moving the player.
    //If the "sneak" variable is true, you could reduce the speed by a certain amount (e.g. by multiplying it by 0.5) before calling the controller.Move() method.

    public bool sneak = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //The Update() method is where the player's movement is controlled.
        //It starts by getting the input from the horizontal and vertical axes using the Input.GetAxis("Horizontal") and Input.GetAxis("Vertical") methods.
        //These values are stored in the x and z variables respectively.

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //A Vector3 variable named move is then created by combining the transform.right and transform.forward vectors with the x and z input values respectively.
        //This creates a vector that represents the direction in which the player should move

        Vector3 move = transform.right * x + transform.forward * z;


        // Left CTRL to sneak

        if (Input.GetKey(KeyCode.LeftControl))
        {
            sneak = true;
        }
        else
        {
            sneak = false;
        }

        if (sneak)
        {
            move *= 0.5f;
        }


        //Finally, the controller.Move() method is called, passing in the move vector multiplied by the speed variable and the Time.deltaTime.
        //This causes the player object to move in the direction specified by the move vector, at a speed determined by the speed variable and in a smooth manner.

        controller.Move(move * speed * Time.deltaTime);

    }
}
