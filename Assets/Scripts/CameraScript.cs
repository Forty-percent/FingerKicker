using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraScript : MonoBehaviour
{
    public float keyboardMoveSpeed = 10.0f;
    public float keyboardTurnSpeed = 5.0f;

    public float mouseMoveSpeed = 50.0f;
    public float mouseScrollSpeed = 20.0f;
    public float mouseTurnSpeed = 4.0f;

    public float minTurnAngle = -90.0f;
    public float maxTurnAngle = 90.0f;
    
    private float rotX;


    bool rightMouseBtnPress = false;
    bool leftMouseBtnPress = false;
  
    void Start() {
        keyboardMoveSpeed = 10.0f;
    }

    void Update()
    {
        MouseAiming();
        KeyboardMovement();
    }


    void MouseAiming()
    {
        // Left mouse button 
        if (Input.GetMouseButtonDown(0))
        {
            leftMouseBtnPress = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            leftMouseBtnPress = false;
        }


        // Right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            rightMouseBtnPress = true; 
        }
        if (Input.GetMouseButtonUp(1))
        {
            rightMouseBtnPress = false;
        }


        // If BOTH buttons pressed then move the camera forward/backward based on the Y mouse movement
        if (leftMouseBtnPress && rightMouseBtnPress )
        {
            // move forward
            Vector3 dir = new Vector3(-Input.GetAxis("Mouse X"), 0, mouseScrollSpeed * Input.GetAxis("Mouse Y"));
            transform.Translate(dir);
        } 
        else if (leftMouseBtnPress)
        {
            // Rotate the camera round
            float y = Input.GetAxis("Mouse X") * mouseTurnSpeed;
            rotX += Input.GetAxis("Mouse Y") * mouseTurnSpeed;
            // clamp the vertical rotation
            rotX = Mathf.Clamp(rotX, minTurnAngle, maxTurnAngle);
            // rotate the camera
            transform.eulerAngles = new Vector3(-rotX, transform.eulerAngles.y + y, 0);
        }
        else if (rightMouseBtnPress)
        {
            // Move the camera 
            Vector3 dir = new Vector3(0, 0, 0);
            dir.x = -Input.GetAxis("Mouse X");
            dir.y = Input.GetAxis("Mouse Y");
            transform.Translate(dir * mouseMoveSpeed * Time.deltaTime);
        }
        else
        {
            // Handle tthe scrollwheel
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                Vector3 dir = new Vector3(0, 0, mouseScrollSpeed * (Input.GetAxis("Mouse ScrollWheel")));
                transform.Translate(dir);
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                Vector3 dir = new Vector3(0, 0, mouseScrollSpeed * (Input.GetAxis("Mouse ScrollWheel")));
                transform.Translate(dir);
            }
        }
    }
    void KeyboardMovement()
    {
        Vector3 pos = transform.position;
        if (Input.GetKey("w"))
        {
            pos.z += keyboardMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos.z -= keyboardMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos.x += keyboardMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos.x -= keyboardMoveSpeed * Time.deltaTime;
        }

        //  Vector3 dir = new Vector3(0, 0, 0);
        //  dir.x = Input.GetAxis("Horizontal");
        //  dir.z = Input.GetAxis("Vertical");
        //  transform.Translate(dir * keyboardMoveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            pos.y += keyboardMoveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            pos.y -= keyboardMoveSpeed * Time.deltaTime;
        }

        // Set the position
        transform.position = pos;


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, keyboardTurnSpeed, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, -keyboardTurnSpeed, 0);
        }


        //  if (Input.GetKey(KeyCode.UpArrow))
        //  {
        //        transform.Rotate(keyboardTurnSpeed, 0, 0);
        //  }

        //  if (Input.GetKey(KeyCode.DownArrow))
        //  {
        //        transform.Rotate(-keyboardTurnSpeed, 0, 0);
        //  }

    }
}