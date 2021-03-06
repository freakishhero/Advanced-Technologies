﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //The movement speed of the camera when controlled
    float movement_speed = 20.0f;

    //The boundary around the screen that the mouse will move the camera within
    float camera_border_threshold = 15.0f;

    //The speed for mousewheel scrolling
    float mouse_scroll_speed = 4.0f;

    //The x and z (perceived y) limits to clamp camera movement at
    Vector2 camera_limit;

	// Use this for initialization
	void Start () {
	    camera_limit = new Vector2(15, 15);
    }
	
	// Update is called once per frame
	void Update () {
        //The cameras current position
        Vector3 position = transform.position;

        if(Input.GetKey("w") || Input.GetKey("up") 
        || Input.mousePosition.y >= Screen.height - camera_border_threshold)
        {
            position.z += movement_speed * Time.deltaTime;
        }

        if (Input.GetKey("s") || Input.GetKey("down") 
        || Input.mousePosition.y <= camera_border_threshold)
        {
            position.z -= movement_speed * Time.deltaTime;
        }

        if (Input.GetKey("a") || Input.GetKey("left")
        || Input.mousePosition.x <= camera_border_threshold)
        {
            position.x -= movement_speed * Time.deltaTime;
        }

        if (Input.GetKey("d") || Input.GetKey("right")
        || Input.mousePosition.x >= Screen.width - camera_border_threshold)
        {
            position.x += movement_speed * Time.deltaTime;
        }

        float mouse_scroll_wheel = Input.GetAxis("Mouse ScrollWheel");
        position.y -= mouse_scroll_wheel * mouse_scroll_speed * Time.deltaTime;

        position.x = Mathf.Clamp(position.x, -camera_limit.x, camera_limit.x);
        position.y = Mathf.Clamp(position.y, 30.0f, 50.0f);
        position.z = Mathf.Clamp(position.z, -camera_limit.y, camera_limit.y);

        transform.position = position;
    }
}
