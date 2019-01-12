using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JelloController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb2D;

    public bool jumping;

    public int jumpHeight = 5;
    public float fallMultiplier = 2.5f;
    public int speed = 5;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            animator.SetBool("HeavyAttack", true);
        if (Input.GetKeyUp(KeyCode.X))
            animator.SetBool("HeavyAttack", false);
        if (Input.GetKeyDown(KeyCode.C))
            animator.SetTrigger("LightAttack");

        rb2D.AddRelativeForce(new Vector2(Input.GetAxis("Horizontal") * speed , 0), ForceMode2D.Impulse);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            rb2D.AddRelativeForce(new Vector2(0, jumpHeight * 10), ForceMode2D.Impulse);
    }

    public void FixedUpdate()
    {
        if (rb2D.velocity.y < 0)
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }
}
