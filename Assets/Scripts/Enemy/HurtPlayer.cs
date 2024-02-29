using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HurtPlayer : MonoBehaviour
{
    private HealthManager healthSlime;
    private PlayerController playerController; 
    private float waitToHurt = 2f;
    private bool isTouching;

    [SerializeField]
    private int damgeToGive = 10;

    void Start()
    {
        healthSlime = FindObjectOfType<HealthManager>();
        playerController = FindObjectOfType<PlayerController>(); 
    }

    void Update()
    {
        if (isTouching)
        {
            waitToHurt -= Time.deltaTime;
            if (waitToHurt <= 0)
            {
                healthSlime.HurtPlayer(damgeToGive);
                Vector3 knockbackDirection = (playerController.transform.position - transform.position).normalized; // Tính toán hướng đẩy lùi
                playerController.Knockback(knockbackDirection); // Gọi hàm Knockback
                waitToHurt = 2f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            other.gameObject.GetComponent<HealthManager>().HurtPlayer(damgeToGive);
            Vector3 knockbackDirection = (playerController.transform.position - transform.position).normalized; // Tính toán hướng đẩy lùi
            playerController.Knockback(knockbackDirection); // Gọi hàm Knockback
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            isTouching = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            isTouching = false;
            waitToHurt = 2f;
        }
    }
}
