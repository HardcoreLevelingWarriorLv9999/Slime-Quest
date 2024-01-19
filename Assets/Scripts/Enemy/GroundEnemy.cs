using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    private Animator myAnimate;
    private Transform target;

    public Transform homePos;
    [SerializeField]
    private float speed = 0f;
    [SerializeField]
    private float maxRange = 0f;
    [SerializeField]
    private float minRange = 0f;




    // Start is called before the first frame update
    void Start()
    {
        myAnimate = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;

    }

    // Update is called once per frame
    void Update()
    {
        //khi rời khỏi phạm vi cố định thì sẽ trở lại vị trí đặt sẵn
        if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange)
        {
            FollowPlayer();
        }
        else if (Vector3.Distance(target.position, transform.position) >= maxRange)
        {
            GoHome();
        }
    }

    public void FollowPlayer()
    {
        myAnimate.SetBool("isMoving", true);
        myAnimate.SetFloat("moveX", target.position.x - transform.position.x);
        myAnimate.SetFloat("moveY", target.position.y - transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
    public void GoHome()
    {
        myAnimate.SetFloat("moveX", homePos.position.x - transform.position.x);
        myAnimate.SetFloat("moveY", homePos.position.y - transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position, homePos.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, homePos.position) == 0)
        {
            myAnimate.SetBool("isMoving", false);
        }
    }

}