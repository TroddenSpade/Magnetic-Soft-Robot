using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]

public class MagneticFieldScriptableObject : ScriptableObject
{
    public bool isFieldOn = false;
    public float fieldMagnitude = 0.0f;

    public Vector3 magneticFieldOrientation = Vector3.up;
    public Vector3 magneticFieldUp = -Vector3.forward;
    public Vector3 magneticFieldRight = Vector3.right;

    public void reset(Vector3 forward, Vector3 right, Vector3 up)
    {
        isFieldOn = false;
        fieldMagnitude = 9.6f;

        magneticFieldOrientation = up;
        magneticFieldUp = -forward;
        magneticFieldRight = right;
    }

    public void setMagneticFieldState(bool state)
    {
        isFieldOn = state;
    }

    public void changeMagneticFieldState(){
        isFieldOn = !isFieldOn;
    }

    public void setMagneticField(Vector3 orientation){
        magneticFieldOrientation = orientation;
    }

    public void applyQuaternion(Quaternion q, bool reset=true){
        if(reset){
            magneticFieldOrientation = q * Vector3.forward;
            magneticFieldUp = q * Vector3.up;
            magneticFieldRight = q * Vector3.right;
        }else{
            magneticFieldOrientation = q * magneticFieldOrientation;
            magneticFieldUp = q * magneticFieldUp;
            magneticFieldRight = q * magneticFieldRight;
        }
    }

    public void rotateEuler(Quaternion q)
    {
        magneticFieldOrientation = q * magneticFieldOrientation;
        magneticFieldUp = q * magneticFieldUp;
        magneticFieldRight = q * magneticFieldRight;
    }

    public void rotate(float angle, int direction){
        if(direction == 0){
            magneticFieldOrientation = Quaternion.AngleAxis(angle, magneticFieldOrientation) * magneticFieldOrientation;
            magneticFieldUp = Quaternion.AngleAxis(angle, magneticFieldOrientation) * magneticFieldUp;
            magneticFieldRight = Quaternion.AngleAxis(angle, magneticFieldOrientation) * magneticFieldRight;
        }
        if(direction == 1){
            magneticFieldOrientation = Quaternion.AngleAxis(angle, magneticFieldRight) * magneticFieldOrientation;
            magneticFieldUp = Quaternion.AngleAxis(angle, magneticFieldRight) * magneticFieldUp;
            magneticFieldRight = Quaternion.AngleAxis(angle, magneticFieldRight) * magneticFieldRight;
        }
        if(direction == 2){
            magneticFieldOrientation = Quaternion.AngleAxis(angle, magneticFieldUp) * magneticFieldOrientation;
            magneticFieldUp = Quaternion.AngleAxis(angle, magneticFieldUp) * magneticFieldUp;
            magneticFieldRight = Quaternion.AngleAxis(angle, magneticFieldUp) * magneticFieldRight;
        }
    }

    public Vector3 getOrientation(bool debug=false){
        if(isFieldOn || debug){
            return magneticFieldOrientation;
        }else{
            return Vector3.zero;
        }
    }

    public Vector3 getRight(){
        return magneticFieldRight;
    }

    public Vector3 getUp(){
        return magneticFieldUp;
    }

    public float getFieldMagnitude(){
        return fieldMagnitude;
    }

    public bool isOn(){
        return isFieldOn;
    }
}
