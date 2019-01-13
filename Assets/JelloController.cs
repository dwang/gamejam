using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JelloController : MonoBehaviour
{
    [Header("Options")]
    [SerializeField]
    private int maxJumps = 3;
    [SerializeField]
    private int originalSpeed = 5;
    [SerializeField]
    private int originalJumpHeight = 5;
    [SerializeField]
    private float fallMultiplier = 2.5f;
    public string moveXAxis;
    public string moveYAxis;
    public string jumpButton;
    public string heavyAttack;
    public string lightAttack;
    public string dodgeButton;

    [Header("Components")]
    public Animator animator;
    public Rigidbody2D rb2D;
    public SpriteRenderer spriteRenderer;

    [Header("Misc")]
    public int jumpHeight;
    public int speed;
    public bool canMove = true;
    public bool facingRight = true;
    public bool grounded;
    public int jumpsLeft;

    public Transform groundCheck;
    public LayerMask groundLayer;

    public void Construct(string moveXAxis, string moveYAxis, string jumpButton, string heavyAttack, string lightAttack, string dodgeButton)
    {
        this.moveXAxis = moveXAxis;
        this.moveYAxis = moveYAxis;
        this.jumpButton = jumpButton;
        this.heavyAttack = heavyAttack;
        this.lightAttack = lightAttack;
        this.dodgeButton = dodgeButton;
    }

    public void Awake()
    {
        speed = originalSpeed;
        jumpsLeft = maxJumps;
        jumpHeight = originalJumpHeight;
    }

    public void Update()
    {
        spriteRenderer.flipX = !facingRight;

        if (canMove)
        {
            if (Input.GetAxis(moveXAxis) == 1.0f)
                facingRight = true;
            else if (Input.GetAxis(moveXAxis) == -1.0f)
                facingRight = false;

            if (Input.GetButtonDown(heavyAttack))
                StartCoroutine(HeavyAttack());
            if (Input.GetButtonDown(lightAttack))
                StartCoroutine(LightAttack());

            rb2D.velocity = new Vector2(Input.GetAxis(moveXAxis) * speed, rb2D.velocity.y);

            if (Input.GetButtonDown(jumpButton) && jumpsLeft > 0)
            {
                rb2D.AddRelativeForce(new Vector2(0, jumpHeight * 10), ForceMode2D.Impulse);
                jumpsLeft--;
            }

            //if (Input.GetButtonDown(dodgeButton))
        }
    }

    public void FixedUpdate()
    {
        bool pastFrameGrounded = grounded;
        grounded = false;
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f, groundLayer);
        for (int i = 0; i < colliders.Length; i++)
            if (colliders[i].gameObject != gameObject)
            {
                if (!pastFrameGrounded)
                    OnGrounded();
                grounded = true;
            }

        if (rb2D.velocity.y < 0)
            rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
    }

    public void OnGrounded()
    {
        jumpsLeft = maxJumps;
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

        canMove = false;
        animator.SetBool("HeavyAttack", true);
        while (curTime < 3.0f)
        {
            curTime += Time.deltaTime;
            if (!Input.GetKey(KeyCode.X))
                keyUp = true;
            if (Input.GetKeyDown(KeyCode.Y))
                Debug.Log("Y Pressed");

            yield return new WaitForEndOfFrame();
        }

        while (!keyUp)
        {
            if (!Input.GetKey(KeyCode.X))
            {
                keyUp = true;
                Debug.Log("hit");
            }
           
            yield return new WaitForEndOfFrame();
        }

        Debug.Log("Checkpoint");
        animator.SetBool("HeavyAttack", false);
        yield return new WaitForSeconds(2.0f);

        canMove = true;
    }
}

