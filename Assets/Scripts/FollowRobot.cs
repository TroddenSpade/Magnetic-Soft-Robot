using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRobot : MonoBehaviour
{
    private Vector3 offset;
    public GameObject robot;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0.0f, 0.1f, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            offset += transform.forward * Time.deltaTime;
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            offset -= transform.forward * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.Q))
        {
            offset = Quaternion.Euler(new Vector3(0.0f, 90.0f * Time.deltaTime, 0.0f)) * (offset);
            transform.Rotate(0.0f, 90.0f * Time.deltaTime, 0.0f);
        }
        if(Input.GetKey(KeyCode.E))
        {
            offset = Quaternion.Euler(new Vector3(0.0f, -90.0f * Time.deltaTime, 0.0f)) * (offset);
            transform.Rotate(0.0f, -90.0f * Time.deltaTime, 0.0f);
        }

        transform.position = robot.transform.position + offset;

        // transform.position = robot.transform.position - 0.4f*robot.transform.forward + 0.05f*robot.transform.up;
        // transform.rotation = robot.transform.rotation;
    }
}
