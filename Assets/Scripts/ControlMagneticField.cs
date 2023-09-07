using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMagneticField : MonoBehaviour
{
    public MagneticFieldScriptableObject mfield;
    public Transform robot;

    // Start is called before the first frame update
    void Start()
    {
        mfield.reset(robot.forward, robot.right, robot.up);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            mfield.changeMagneticFieldState();
        }

        if (Input.GetKey(KeyCode.W))
        {
            mfield.rotate(+40*Time.fixedDeltaTime, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            mfield.rotate(-40*Time.fixedDeltaTime, 1);
        }

        if (Input.GetKey(KeyCode.D))
        {
            mfield.rotate(+40*Time.fixedDeltaTime, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            mfield.rotate(-40*Time.fixedDeltaTime, 0);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            mfield.rotate(+40*Time.fixedDeltaTime, 2);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            mfield.rotate(-40*Time.fixedDeltaTime, 2);
        }

        if(Input.GetKey(KeyCode.Keypad8))
        {
            mfield.fieldMagnitude += 0.05f * Time.fixedDeltaTime;
        }
        if(Input.GetKey(KeyCode.Keypad2))
        {
            mfield.fieldMagnitude -= 0.05f * Time.fixedDeltaTime;
        }
    }
}
