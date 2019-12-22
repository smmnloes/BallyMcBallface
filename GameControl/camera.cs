using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public float yOffset;


    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(Globals.ball.transform.position.x, Globals.ball.transform.position.y + yOffset,
            transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(Globals.ball.transform.position.x, Globals.ball.transform.position.y + yOffset,
            transform.position.z);
    }
}