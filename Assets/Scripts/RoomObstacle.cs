using UnityEngine;

public class RoomObstacle : MonoBehaviour
{
    public GameObject[] enemies;

    private void Update()
    {
        if (CheckAllEnemiesDefeated())
        {
            gameObject.SetActive(false);
        }
    }

    private bool CheckAllEnemiesDefeated()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                return false;
            }
        }
        return true;
    }
}
