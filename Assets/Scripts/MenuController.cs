using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void BatDau()
    {
        SceneManager.LoadScene(0);

    }
    public void Thoat()
    {
        //Application.Quit();
        Debug.Log("da thoat");
    }
}
