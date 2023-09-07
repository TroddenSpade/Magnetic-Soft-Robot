using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PartialMagneticField : MonoBehaviour
{
    public int phase;
    public MagneticFieldScriptableObject mfield;

    private Transform capsule; 
    private Rigidbody rb;

    public bool isFieldOn;
    public float fieldMagnitude;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsule = gameObject.transform.GetChild(0);
    }


    void FixedUpdate()
    {
        rb.AddForce(mfield.getFieldMagnitude() * Vector3.up);

        Vector3 torque = Vector3.Cross(Quaternion.AngleAxis(-phase, capsule.forward) * capsule.up, mfield.getOrientation());
        rb.AddTorque(torque);

        // Debug.DrawRay(capsule.position, Quaternion.AngleAxis(-phase, capsule.forward) * capsule.up, Color.red);

        if(phase == 0){
            Debug.DrawRay(capsule.position, mfield.getOrientation(true), Color.cyan);
            Debug.DrawRay(capsule.position, -mfield.getOrientation(true), Color.green);           
        }

        isFieldOn = mfield.isFieldOn;
        fieldMagnitude = mfield.getFieldMagnitude();
    }
}
