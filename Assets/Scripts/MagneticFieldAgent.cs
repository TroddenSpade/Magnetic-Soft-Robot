using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents; 
using Unity.MLAgents.Sensors; 
using Unity.MLAgents.Actuators;

public class MagneticFieldAgent : Agent
{
    public MagneticFieldScriptableObject mfield;
    // eleven bodies
    public Transform body0;
    public Transform body1;
    public Transform body2;
    public Transform body3;
    public Transform body4;
    public Transform body5;
    public Transform body6;
    public Transform body7;
    public Transform body8;
    public Transform body9;
    public Transform body10;

    private List<Transform> bodies = new List<Transform>();
    
    private Vector3[] data;
    private float distance_to_point = 0.08f;
    private int current_point = 0;

    private float magtitude_base = 9.7f;
    private float targetSpeed = 5.0f;

    private Vector3 initial_pos;
    private Quaternion initial_rot;
    private GameObject sphere;

    // load x, y, z data into an array of Vector3
    public Vector3[] readData(string filename)
    {
        string[] lines = System.IO.File.ReadAllLines(filename);
        data = new Vector3[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            string[] line = lines[i].Split(',');
            data[i] = new Vector3(float.Parse(line[0]), float.Parse(line[1]), float.Parse(line[2]));
        }
        return data;
    }

    void Start()
    {
        data = readData("Assets/points.txt");
        initial_pos = body0.position;
        initial_rot = body0.rotation;
        bodies.Add(body0);
        bodies.Add(body1);
        bodies.Add(body2);
        bodies.Add(body3);
        bodies.Add(body4);
        bodies.Add(body5);
        bodies.Add(body6);
        bodies.Add(body7);
        bodies.Add(body8);
        bodies.Add(body9);
        bodies.Add(body10);

        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.GetComponent<SphereCollider>().enabled = false;
        sphere.transform.position = data[0];
        sphere.transform.localScale = new Vector3(0.01f,0.01f,0.01f);
    }

    public override void OnEpisodeBegin()
    {
        body0.position = initial_pos;
        body0.rotation = initial_rot;
        mfield.reset(body0.forward, body0.right, body0.up);
        mfield.setMagneticFieldState(true);
        current_point = 0;
    }

    // check whether the robot is close enough to the target point
    private bool isCloseEnough(Vector3 target)
    {
        Vector3 center = (body0.position + body1.position + body2.position + body3.position + body4.position + 
                          body6.position + body7.position + body8.position + body9.position + body10.position) / 10;
        return Vector3.Distance(center, target) < distance_to_point;
    }

    void FixedUpdate()
    {
        if (isCloseEnough(data[current_point]))
        {
            current_point++;
            Debug.Log(current_point);
            sphere.transform.position = data[current_point];
            if (current_point == data.Length)
            {
                EndEpisode();
            }
        }
        else
        {
            Vector3 avg_right = Vector3.zero;
            Vector3 center = Vector3.zero;
            Vector3 avg_vel = Vector3.zero;
            for (int i = 0; i < bodies.Count; i++)
            {
                avg_right += bodies[i].forward;
                avg_vel += bodies[i].gameObject.GetComponent<Rigidbody>().velocity;
                if(i==5)
                    continue;
                center += bodies[i].position;
            }
            avg_right /= bodies.Count;
            center /= (bodies.Count-1);
            avg_vel /= bodies.Count;

            Vector3 norm_target = Vector3.Normalize(data[current_point]-center);
            Vector3 right = Vector3.Cross(Vector3.up, norm_target);
            float angle_reward = Mathf.Abs(Vector3.Dot(right, avg_right));

            Vector3 vel_dir = norm_target * targetSpeed;
            var velDeltaMagnitude = Mathf.Clamp(Vector3.Distance(vel_dir, avg_vel), 0, targetSpeed);
            float vel_reward = Mathf.Pow(1 - Mathf.Pow(velDeltaMagnitude / targetSpeed, 2), 2);

            AddReward(vel_reward * angle_reward);
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float[] action = new float[3];
        action[0] = Mathf.Clamp(actions.ContinuousActions[0], -1, 1);
        action[1] = Mathf.Clamp(actions.ContinuousActions[1], -1, 1);
        action[2] = Mathf.Clamp(actions.ContinuousActions[2], -1, 1);

        Quaternion q = Quaternion.Euler(action[0], 
                                        action[1], 
                                        action[2]);
        mfield.rotateEuler(q);
        mfield.fieldMagnitude = actions.ContinuousActions[3]/10.0f + magtitude_base;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(body0.position);
        sensor.AddObservation(body0.rotation);
        sensor.AddObservation(body0.gameObject.GetComponent<Rigidbody>().velocity);
        sensor.AddObservation(body1.position);
        sensor.AddObservation(body1.rotation);
        sensor.AddObservation(body1.gameObject.GetComponent<Rigidbody>().velocity);
        sensor.AddObservation(body2.position);
        sensor.AddObservation(body2.rotation);
        sensor.AddObservation(body2.gameObject.GetComponent<Rigidbody>().velocity);
        sensor.AddObservation(body3.position);
        sensor.AddObservation(body3.rotation);
        sensor.AddObservation(body3.gameObject.GetComponent<Rigidbody>().velocity);
        sensor.AddObservation(body4.position);
        sensor.AddObservation(body4.rotation);
        sensor.AddObservation(body4.gameObject.GetComponent<Rigidbody>().velocity);
        sensor.AddObservation(body5.position);
        sensor.AddObservation(body5.rotation);
        sensor.AddObservation(body5.gameObject.GetComponent<Rigidbody>().velocity);
        sensor.AddObservation(body6.position);
        sensor.AddObservation(body6.rotation);
        sensor.AddObservation(body6.gameObject.GetComponent<Rigidbody>().velocity);
        sensor.AddObservation(body7.position);
        sensor.AddObservation(body7.rotation);
        sensor.AddObservation(body7.gameObject.GetComponent<Rigidbody>().velocity);
        sensor.AddObservation(body8.position);
        sensor.AddObservation(body8.rotation);
        sensor.AddObservation(body8.gameObject.GetComponent<Rigidbody>().velocity);
        sensor.AddObservation(body9.position);
        sensor.AddObservation(body9.rotation);
        sensor.AddObservation(body9.gameObject.GetComponent<Rigidbody>().velocity);
        sensor.AddObservation(body10.position);
        sensor.AddObservation(body10.rotation);
        sensor.AddObservation(body10.gameObject.GetComponent<Rigidbody>().velocity);

        sensor.AddObservation(mfield.getOrientation(true));
        sensor.AddObservation((mfield.fieldMagnitude - magtitude_base)*10f);

        Vector3 center = (body0.position + body1.position + body2.position + body3.position + body4.position + 
                          body6.position + body7.position + body8.position + body9.position + body10.position) / 10;
        sensor.AddObservation(data[current_point] - center);
    }


    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        if(current_point == 224 || current_point == 162){
            continuousActionsOut[3] = 1.05f;
        }else{
            continuousActionsOut[3] = 1f;
        }

        Vector3 avg_right = Vector3.zero;
        Vector3 center = Vector3.zero;
        for (int i = 0; i < bodies.Count; i++)
        {
            avg_right += bodies[i].forward;
            if(i==5)
                continue;
            center += bodies[i].position;
        }
        avg_right /= bodies.Count;
        center /= (bodies.Count-1);
        
        Vector3 norm_target = Vector3.Normalize(data[current_point]-center);
        Vector3 right = Vector3.Cross(Vector3.up, norm_target);
        Vector3 forw = Vector3.Cross(right, Vector3.up);
        Debug.DrawRay(body0.position, norm_target, Color.red);

        float angle = Vector3.Angle(Vector3.up, mfield.getOrientation(true));
        Quaternion target_angle = Quaternion.AngleAxis(Mathf.Sign(Vector3.Dot(forw, mfield.getOrientation(true))) * angle + 10f, right);
        Vector3 target_vector = target_angle * Vector3.up;
        // Debug.DrawRay(body0.position, target_vector, Color.yellow);
        
        Quaternion q = Quaternion.FromToRotation(mfield.getOrientation(true), target_vector);
        // Quaternion q = Quaternion.AngleAxis(20, right);
        continuousActionsOut[0] = 180f-q.eulerAngles.x >=0 ?
                                Mathf.Clamp(q.eulerAngles.x, 0f, 5f)/5f : Mathf.Clamp(q.eulerAngles.x-360, -5f, 0f)/5f;
        continuousActionsOut[1] = 180f-q.eulerAngles.y >=0 ?
                                Mathf.Clamp(q.eulerAngles.y, 0f, 5f)/5f : Mathf.Clamp(q.eulerAngles.y-360, -5f, 0f)/5f;
        continuousActionsOut[2] = 180f-q.eulerAngles.z >=0 ?
                                Mathf.Clamp(q.eulerAngles.z, 0f, 5f)/5f : Mathf.Clamp(q.eulerAngles.z-360, -5f, 0f)/5f;
    }
}
