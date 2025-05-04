using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Dynamic;

public class PlayerBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Animator animator;
    public Rigidbody2D rb;
    public float jumpheight = 5f;
    public bool isGround = true;

    private float movement;
    public float moveSpeed = 5f;
    private bool facingRight = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");

        if (movement < 0f && facingRight)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);
            facingRight = false;

        }
        else if (movement > 0f && facingRight == false)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            facingRight = true;

        }

        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            Jump();
            isGround = false;
            animator.SetBool("Jump", true);
        }

        if (Mathf.Abs(movement) > .1f)
        {
            animator.SetFloat("Run", 1f);
        }
        else if (movement < .1f)
        {
            animator.SetFloat("Run", 0f);
        }
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(movement, 0f, 0f) * Time.fixedDeltaTime * moveSpeed;
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpheight), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
            animator.SetBool("Jump", false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Exit")
        {

            FindObjectOfType<SceneManageMent>().LoadExit();
        }



    }
}
