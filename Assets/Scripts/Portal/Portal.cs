using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private Animator transitionAnim;
    public string targetSceneName; // Tên của scene đích

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Kiểm tra va chạm với người chơi
        {
            // Bắt đầu coroutine để chờ 1 giây
            StartCoroutine(LoadSceneWithDelay(targetSceneName));
        }
    }

    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        transitionAnim.SetTrigger("End");
        // Chờ 1 giây
        yield return new WaitForSeconds(1f);

        // Tải scene đích
        SceneManager.LoadScene(sceneName);
        transitionAnim.SetTrigger("Start");

        // Đặt vị trí của người chơi trong scene đích (ví dụ: vị trí của cổng)
        Vector3 targetPosition = new Vector3(10f, 5f, 0f); // Điều chỉnh vị trí tùy theo yêu cầu
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = targetPosition;
        }
    }

    void ReMove()
    {
        Destroy(gameObject);
    }
}
