using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform p1;
    public Transform p2;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3((p1.position.x + p2.position.x) / 2, (p1.position.y + p2.position.y) / 2, -10);
    }
}
