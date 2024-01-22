using System.Collections;
using UnityEngine;

public class BeeEnemy : MonoBehaviour
{
    private Animator beeAnimate;
    public Transform player;  // Tham chiếu đến người chơi
    public Transform homePos;  // Tham chiếu đến vị trí đặt sẵn

    [Header("Attack Setting")]
    [SerializeField]
    private float speed = 10f;  // Tốc độ di chuyển của kẻ thù
    private float attackTime = 2f;  // Thời gian giữa các cuộc tấn công
    private float nextAttackTime;
    [SerializeField]
    private float maxRange = 0f;
    [SerializeField]
    private float minRange = 0f;

    private void Start()
    {
        beeAnimate = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Vector3.Distance(player.position, transform.position) <= maxRange && Vector3.Distance(player.position, transform.position) >= minRange)
        {
            if (Time.time >= nextAttackTime)
            {
                StartCoroutine(DashToTarget(player.position));
                nextAttackTime = Time.time + attackTime;
            }
        }
        else if (Vector3.Distance(player.position, transform.position) >= maxRange)
        {
            GoHome();
        }
    }

    IEnumerator DashToTarget(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        float dashTime = 0.2f;

        beeAnimate.SetBool("bIsMoving", true);
        beeAnimate.SetFloat("bMoveX", direction.x);
        beeAnimate.SetFloat("bMoveY", direction.y);

        while (dashTime > 0)
        {
            transform.position += direction * speed * Time.deltaTime;
            dashTime -= Time.deltaTime;
            yield return null;
        }

        beeAnimate.SetBool("bIsMoving", false);
    }

    public void GoHome()
    {
        Vector3 direction = (homePos.position - transform.position).normalized;

        beeAnimate.SetFloat("bMoveX", direction.x);
        beeAnimate.SetFloat("bMoveY", direction.y);
        transform.position = Vector3.MoveTowards(transform.position, homePos.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, homePos.position) == 0)
        {
            beeAnimate.SetBool("bIsMoving", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ultimate")
        {
            Vector2 difference = transform.position - other.transform.position;
            transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
        }
    }
}
