using UnityEngine;

public class BossController : MonoBehaviour
{
    public float speed = 2f;
    private float startSpeed;
    public bool isSpecial = false;
    public float specialAbilityCooldown = 10f; // Thời gian chờ kỹ năng đặc biệt (tính bằng giây)

    private Transform player;
    private PlayerController playerController;
    private float specialAbilityTimer; // Đếm thời gian chờ kỹ năng đặc biệt

    private Animator bossAnimator; 
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = FindObjectOfType<PlayerController>();
        bossAnimator = GetComponent<Animator>();
        specialAbilityTimer = specialAbilityCooldown; // Khởi tạo đếm thời gian
        startSpeed = speed;
    }

    private void Update()
    {
        // Cập nhật hành vi của con boss
        if (isSpecial)
        {
            // Nếu đang sử dụng kỹ năng đặc biệt, không tính thời gian
            specialAbilityTimer = 0f;
            speed = 6f; // Tăng tốc độ khi đặc biệt
        }
        else
        {
            // Hành vi thông thường: đếm ngược thời gian chờ kỹ năng đặc biệt
            specialAbilityTimer -= Time.deltaTime;
            if (specialAbilityTimer <= 0f)
            {
                // Kỹ năng đặc biệt đã sẵn sàng để kích hoạt
                isSpecial = true;
                specialAbilityTimer = specialAbilityCooldown; // Đặt lại đếm thời gian
            }
        }

        // Di chuyển con boss theo người chơi
        Vector3 moveDirection = (player.position - transform.position).normalized;
        transform.Translate(moveDirection * speed * Time.deltaTime);

        bossAnimator.SetFloat("moveX", moveDirection.x);
        bossAnimator.SetFloat("moveY", moveDirection.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isSpecial)
        {
            // Xử lý va chạm với người chơi
            isSpecial = false;
            // Đặt lại tốc độ về giá trị ban đầu
            speed = startSpeed;
            // Tùy chọn, đặt lại đếm thời gian kỹ năng đặc biệt ở đây
            specialAbilityTimer = specialAbilityCooldown;
            Debug.Log("Cham");
        }
    }
}
