using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    private Animator myAnimate;
    private Transform target;

    public Transform homePos;
    public Transform secondPos;
    private Transform nextPos;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxRange;
    [SerializeField]
    private float minRange;

    private EnemyHealthManager enemyHealthManager;

    // Start is called before the first frame update
    void Start()
    {
        myAnimate = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;
        nextPos = homePos;
        enemyHealthManager = GetComponent<EnemyHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyHealthManager.isDefeated && Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange)
        {
            FollowPlayer();
        }
        else if (Vector3.Distance(target.position, transform.position) >= maxRange)
        {
            MoveToNextPos();
        }
    }

    public void FollowPlayer()
    {
        myAnimate.SetBool("isMoving", true);
        myAnimate.SetFloat("moveX", target.position.x - transform.position.x);
        myAnimate.SetFloat("moveY", target.position.y - transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
    
    public void MoveToNextPos()
    {
        myAnimate.SetFloat("moveX", nextPos.position.x - transform.position.x);
        myAnimate.SetFloat("moveY", nextPos.position.y - transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position, nextPos.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, nextPos.position) == 0)
        {
            myAnimate.SetBool("isMoving", true);
            nextPos = nextPos == homePos ? secondPos : homePos;
        }
    }

    //Knockback enemy
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ultimate")
        {
            Vector2 difference = transform.position - other.transform.position;
            transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
        }
    }
}
