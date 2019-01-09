using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            transform.Translate(new Vector3(Input.GetAxis("Horizontal") * 0.25f, 0, Input.GetAxis("Vertical") * 0.25f));
    }
}
