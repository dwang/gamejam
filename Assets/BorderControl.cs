using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderControl : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Jello")
            col.gameObject.transform.position = new Vector2(0, 0);
    }
}
