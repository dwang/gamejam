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
    public bool facingRight = true;
    public SpriteRenderer spriteRenderer;

    public void Update()
    {
        spriteRenderer.flipX = !facingRight;

        if (speed > 0)
        {
            if (Input.GetAxis("Horizontal") == 1.0f)
                facingRight = true;
            else if (Input.GetAxis("Horizontal") == -1.0f)
                facingRight = false;

            if (Input.GetKeyDown(KeyCode.X))
                StartCoroutine(HeavyAttack());
            if (Input.GetKeyDown(KeyCode.C))
                StartCoroutine(LightAttack());

            rb2D.AddRelativeForce(new Vector2(Input.GetAxis("Horizontal") * speed, 0), ForceMode2D.Impulse);

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
                rb2D.AddRelativeForce(new Vector2(0, jumpHeight * 10), ForceMode2D.Impulse);
        }
    }

    public void FixedUpdate()
    {
        if (rb2D.velocity.y < 0)
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }

    public IEnumerator LightAttack()
    {
        speed = 0;
        animator.SetTrigger("LightAttack");
        yield return new WaitForSeconds(1.0f);
        speed = 5;
    }

    public IEnumerator HeavyAttack()
    {
        float curTime = 0.0f;
        bool keyUp = false; 

        speed = 0;
        animator.SetBool("HeavyAttack", true);

        while (curTime < 3.0f)
        {
            curTime += Time.deltaTime;

            if (Input.GetKeyUp(KeyCode.X))
                keyUp = true;

            yield return new WaitForEndOfFrame();
        }

        while (!keyUp)
        {
            if (Input.GetKeyUp(KeyCode.X))
            {
                keyUp = true;
                Debug.Log("hit");
            }
           

            yield return new WaitForEndOfFrame();
        }

        animator.SetBool("HeavyAttack", false);
        yield return new WaitForSeconds(2.0f);

        speed = 5;
    }
}

