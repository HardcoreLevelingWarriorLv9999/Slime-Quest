using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrigger : MonoBehaviour
{
    public GameObject[] obstacles; // Kéo và thả tất cả vật thể chặn cửa vào mảng này trong Inspector

    private void Start()
    {
        // Ẩn tất cả vật thể chặn cửa khi bắt đầu
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Khi người chơi đi qua, hiển thị tất cả vật thể chặn cửa
            foreach (GameObject obstacle in obstacles)
            {
                obstacle.SetActive(true);
            }
        }
    }
}
