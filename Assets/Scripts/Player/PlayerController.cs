using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move Setting")]
    public float moveSpeed = 5f;
    private Vector3 moveInput;

    [Header("Run Setting")]
    public float runBoost = 2f;
    //Thời gian lướt
    private float runTime;
    public float _runTime;
    bool runOnce = false;
    //Thời gian chờ
    private float waitTime = 0.75f;
    private float currentWaitTime;

    [Header("Damage Effect")]
    public float knockbackSpeed = 10f; // Tốc độ đẩy lùi
    public float knockbackDuration = 0.2f; // Thời gian đẩy lùi

    private bool isKnockback; // Kiểm tra xem nhân vật có đang bị đẩy lùi không
    private float knockbackStartTime; // Thời gian bắt đầu đẩy lùi
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
        Ultimate();
    }

    public void Knockback(Vector3 direction)
    {
        isKnockback = true;
        knockbackStartTime = Time.time;
        moveInput = direction * knockbackSpeed;
    }

    
    void Move()
    {
        if (isKnockback)
        {
            if (Time.time >= knockbackStartTime + knockbackDuration)
                isKnockback = false;
            else
                transform.position += moveInput * Time.deltaTime;
        }
        else
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
    }


    void Dash()
    {
        //Lướt
        // Cập nhật thời gian chờ
        if (currentWaitTime > 0)
        {
            currentWaitTime -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && runTime <= 0 && currentWaitTime <= 0 && !animator.GetBool("UltimateAttacking"))
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

    void Ultimate()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            animator.SetBool("UltimateAttacking", true);
            StartCoroutine(PerformUltimate());
        }
    }

    IEnumerator PerformUltimate()
    {
        healthSlime.isInvincible = true;
        yield return new WaitForSeconds(5);
        healthSlime.isInvincible = false;
        animator.SetBool("UltimateAttacking", false);
    }

}
