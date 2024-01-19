using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move Setting")]
    public float moveSpeed = 5f;
    private Vector3 moveInput;
    private Rigidbody2D rb2D;

    [Header("Run Setting")]
    public float runBoost = 2f;
    //Thời gian lướt
    private float runTime;
    public float _runTime;
    bool runOnce = false;
    //Thời gian chờ
    private float waitTime = 0.75f;
    private float currentWaitTime;

    private Animator animator;
    private HealthManager healthSlime;

    void Start()
    {
        animator = GetComponent<Animator>();
        healthSlime = FindObjectOfType<HealthManager>();
    }

    private void Update()
    {
        Move();
        Dash();
    }

    void Move()
    {
        //di chuyển
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize(); // dòng này dùng để khi di chuyển chéo thì sẽ di chuyển theo tốc độ bình thường
        transform.position += moveInput * moveSpeed * Time.deltaTime;

        animator.SetFloat("Walk", moveInput.sqrMagnitude);

        //quay hướng
        if (moveInput.x != 0)
        {
            if (moveInput.x > 0)
                transform.localScale = new Vector3(1, 1, 0);
            else
                transform.localScale = new Vector3(-1, 1, 0);
        }
    }
    void Dash()
    {
        //Lướt
        // Cập nhật thời gian chờ
        if (currentWaitTime > 0)
        {
            currentWaitTime -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && runTime <= 0 && currentWaitTime <= 0)
        {
            animator.SetBool("Run", true);
            moveSpeed += runBoost;
            runTime = _runTime;
            runOnce = true;

            // Đặt lại thời gian chờ
            currentWaitTime = waitTime;

            // Đặt nhân vật vào trạng thái bất tử
            healthSlime.isInvincible = true;
        }

        if (runTime <= 0 && runOnce == true)
        {
            animator.SetBool("Run", false);
            moveSpeed -= runBoost;
            runOnce = false;

            // Đặt nhân vật ra khỏi trạng thái bất tử
            healthSlime.isInvincible = false;
        }
        else
        {
            runTime -= Time.deltaTime;
        }
    }
}
