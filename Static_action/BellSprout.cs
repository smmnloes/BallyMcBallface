using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellSprout : MonoBehaviour
{
    public bool rotating;
    public float maxRotLeft;
    public float maxRotRight;
    public float rotSpeed;
    int rotDirection;


    // Use this for initialization

    void Start()
    {
        rotDirection = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!rotating) return;
        transform.Rotate(0, 0, rotSpeed * Time.deltaTime * rotDirection);
        var angle = transform.localEulerAngles.z;
        angle = (angle > 180) ? angle - 360 : angle;

        if (angle > maxRotLeft)
        {
            rotDirection = -1;
        }

        if (angle < maxRotRight)
        {
            rotDirection = 1;
        }
    }
}