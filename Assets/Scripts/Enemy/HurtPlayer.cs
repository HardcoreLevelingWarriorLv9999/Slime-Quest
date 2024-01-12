using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HurtPlayer : MonoBehaviour
{
    private HealthManager healthSlime;
    private float waitToHurt = 2f;
    private bool isTouching;

    [SerializeField]
    private int damgeToGive = 10;
    // Start is called before the first frame update
    void Start()
    {
        healthSlime = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //     if (reloading)
        //     {
        //         waitToLoad -= Time.deltaTime;
        //         if (waitToLoad <= 0)
        //         {
        //             SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //         }
        //     }

        if (isTouching)
        {
            waitToHurt -= Time.deltaTime;
            if (waitToHurt <= 0)
            {
                healthSlime.HurtPlayer(damgeToGive);
                waitToHurt = 2f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            // Destroy(other.gameObject);
            // other.gameObject.SetActive(false);
            other.gameObject.GetComponent<HealthManager>().HurtPlayer(damgeToGive);
            // reloading = true;
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
