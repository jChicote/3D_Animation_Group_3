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
            transform.position += transform.right * movementX * Time.fixedDeltaTime;
        }

        if (Input.GetButton("Vertical"))
        {
            float movementY = baseSpeed * Input.GetAxis("Vertical");
            transform.position += transform.forward * movementY * Time.fixedDeltaTime;
        }

    }
}
