using UnityEngine;

public class BossController : MonoBehaviour
{
    public float speed = 4f;
    private float startSpeed;
    public bool isSpecial = false;
    public float specialAbilityCooldown = 10f;
    private bool isAttacking = false;
    private Transform player;
    private float specialAbilityTimer;

    private Animator bossAnimator;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bossAnimator = GetComponent<Animator>();
        specialAbilityTimer = specialAbilityCooldown;
        startSpeed = speed;
    }

    private void Update()
    {
        if (isAttacking)
        {
            bossAnimator.SetBool("boIsAttacking", true);
        }
        else
        {
            bossAnimator.SetBool("boIsAttacking", false);
        }

        if (isSpecial)
        {
            // Nếu đang sử dụng kỹ năng đặc biệt, không tính thời gian
            specialAbilityTimer = 0f;
            speed = 6f; // Tăng tốc độ khi đặc biệt
            bossAnimator.SetBool("boSpAttacking", true);
        }
        else
        {
            // đếm ngược thời gian chờ kỹ năng đặc biệt
            specialAbilityTimer -= Time.deltaTime;
            if (specialAbilityTimer <= 0f)
            {
                // Kỹ năng đặc biệt đã sẵn sàng để kích hoạt
                isSpecial = true;
                specialAbilityTimer = specialAbilityCooldown; // Đặt lại đếm thời gian
            }
        }

        Vector3 moveDirection = (player.position - transform.position).normalized;
        transform.Translate(moveDirection * speed * Time.deltaTime);

        bossAnimator.SetFloat("moveX", moveDirection.x);
        bossAnimator.SetFloat("moveY", moveDirection.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isSpecial)
        {
            isSpecial = false;
            speed = startSpeed;
            specialAbilityTimer = specialAbilityCooldown;
            bossAnimator.SetBool("boSpAttacking", false);
            Debug.Log("Cham");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAttacking = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAttacking = false;
        }
    }
}
