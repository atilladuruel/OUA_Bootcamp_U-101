using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float speed;
    private float jumpForce = 7f;
    private float jump = 1f;
    private bool isGround = true;

    private Rigidbody2D rb;
    private Animator animator;

    public GameObject blastPrefab;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {

        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        // Hizi guncelle
        speed = moveInput * moveSpeed;
        // Speed parametresini Animator Controller'a iletebilirsiniz
        animator.SetFloat("Speed", Mathf.Abs(speed));
        //animator.SetFloat("Speed", Mathf.Abs(moveInput));

        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            isGround = false;
            jump = jump * jumpForce;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("Jump", true);
        }
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
        if (moveInput > 0) // Karakter saga dogru hareket ediyor
        {
            transform.localScale = new Vector3(4f, 4f, 1f); // Spritein yuzu saga donuk
        }
        else if (moveInput < 0) // Karakter sola dogru hareket ediyor
        {
            transform.localScale = new Vector3(-4f, 4f, 1f); // Spritein yuzu sola donuk
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("Jump", false);
            isGround = true;
        }
        if (collision.gameObject.CompareTag("Perception"))
        {
            jumpForce = 10;
            collision.gameObject.SetActive(false);
            Instantiate(blastPrefab, transform.position, transform.rotation);
        }
    }

}
