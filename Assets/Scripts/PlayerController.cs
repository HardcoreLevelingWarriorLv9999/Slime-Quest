using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0f;
    private Vector2 moveInput;
    public Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //di chuyển hướng nào tốc độ vẫn như nhau
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();
        rb.velocity = moveInput * moveSpeed * Time.deltaTime;

        //xoay
        if (moveInput.x != 0)
        {
            if (moveInput.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 0);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 0);
            }
        }

        //animator
        animator.SetFloat("Walk", moveInput.sqrMagnitude);
    }
}
