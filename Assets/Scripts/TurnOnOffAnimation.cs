using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffAnimation : MonoBehaviour
{
    public GameObject image;
    void TurnOff()
    {
        image.SetActive(false);
    }

    void TurnOn()
    {
        image.SetActive(true);
    }
}
