using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public GameObject boss; // Gán GameObject của boss trong inspector
    public GameObject portalPrefab; // Gán prefab cổng dịch chuyển của bạn trong inspector
    public Transform spawnPoint; // Kéo GameObject này vào trong inspector để xác định vị trí tạo cổng
    void Update()
    {
        // Kiểm tra nếu boss không còn tồn tại trong scene
        if (boss == null)
        {
            // Tạo cổng dịch chuyển tại vị trí của spawnPoint
            Instantiate(portalPrefab, spawnPoint.position, spawnPoint.rotation);
            // Disable script sau khi tạo cổng để tránh tạo lặp lại
            this.enabled = false;
        }
    }
}
