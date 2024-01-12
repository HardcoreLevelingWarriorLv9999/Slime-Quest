using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    private bool flashActive;
    [SerializeField]
    private float flashLenght = 0f; // Thời gian tồn tại hiệu ứng nhấp nháy
    private float flashCounter = 0f; //bộ đếm thời gian hiệu ứng nháp nháy

    private SpriteRenderer playerSprite;
    // Start is called before the first frame update
    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
 void Update()
{
    //hiệu ứng nhấp nháy theo thời gian
    if (flashActive)
    {
        float[] thresholds = new float[] { .99f, .82f, .66f, .49f, .33f, .16f, 0f };
        for (int i = 0; i < thresholds.Length; i++)
        {
            if (flashCounter > flashLenght * thresholds[i])
            {
                ChangeColor(i % 2 == 0 ? 0f : 1f);
                break;
            }
        }
        if (flashCounter <= 0f)
        {
            ChangeColor(1f);
            flashActive = false;
        }
        flashCounter -= Time.deltaTime;
    }
}

public void ChangeColor(float alpha)
{
    playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, alpha);
}

    public void HurtPlayer(int damgeToGive)
    {
        currentHealth -= damgeToGive;
        flashActive = true;
        flashCounter = flashLenght;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
