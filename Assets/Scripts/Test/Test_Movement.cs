using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Movement : MonoBehaviour
{
    public float baseSpeed = 3f;

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveObject();
    }

    void MoveObject()
    {
        if (Input.GetButton("Horizontal"))
        {
            float movementX = baseSpeed * Input.GetAxis("Horizontal");
            //transform.position += transform.right * movementX * Time.fixedDeltaTime;
            float yaw = movementX * 0.5f;
            transform.Rotate(0, yaw, 0);
        }

        if (Input.GetButton("Vertical"))
        {
            float movementY = baseSpeed * Input.GetAxis("Vertical");
            transform.position += transform.forward * movementY * 0.1f;
        }

    }
}
