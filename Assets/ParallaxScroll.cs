using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    public Transform target;
    public Vector2 speed;

    public void Update()
    {
        transform.position = target.position * speed;
    }
}
