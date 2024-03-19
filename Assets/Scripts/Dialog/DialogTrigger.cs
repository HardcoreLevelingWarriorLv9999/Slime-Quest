using UnityEngine;
using cherrydev;
public class DialogTrigger : MonoBehaviour
{
    public GameObject imageSpriteObject; // Tham chiếu đến GameObject chứa sprite hình ảnh
    private bool inRange = false; // Cờ để theo dõi xem người chơi có trong phạm vi không
    [SerializeField] private DialogBehaviour dialogBehaviour;
    [SerializeField] private DialogNodeGraph dialogNodeGraph;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Giả sử nhân vật có tag "Player"
        {
            inRange = true; // Người chơi trong phạm vi
            // Kích hoạt GameObject chứa sprite hình ảnh
            imageSpriteObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Giả sử nhân vật có tag "Player"
        {
            inRange = false; // Người chơi ra khỏi phạm vi
            // Tắt GameObject chứa sprite hình ảnh
            imageSpriteObject.SetActive(false);
        }
    }

        private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.F))
        {
            StartDialog();
        }
    }

    private void StartDialog()
    {
        // Thực hiện logic hội thoại ở đây
        // Bạn có thể sử dụng DialogManager hoặc bất kỳ hệ thống nào khác bạn đã thiết lập
        // để bắt đầu cuộc trò chuyện với NPC.
        // Ví dụ:
        dialogBehaviour.StartDialog(dialogNodeGraph);
    }
}
