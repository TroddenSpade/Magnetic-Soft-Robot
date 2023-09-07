using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Trace : MonoBehaviour
{
    public bool trace_state;

    private StreamWriter writer;
    private float time = 0.0f;
    public float time_rate = 1.0f;

    public Transform box;

    // Start is called before the first frame update
    void Start()
    {
        string path = "Assets/test.txt";

        writer = new StreamWriter(path, true);
        Debug.Log(writer);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(trace_state){
            time += Time.deltaTime;

            if(time >= time_rate)
            {
                writer.WriteLine(box.position.ToString("F3"));
                time = 0.0f;
            }
        }
    }
}
