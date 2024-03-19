using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cherrydev;
public class TestDialog : MonoBehaviour
{
    [SerializeField] private DialogBehaviour dialogBehaviour;
    [SerializeField] private DialogNodeGraph dialogNodeGraph;

    // Start is called before the first frame update
    void Start()
    {
        dialogBehaviour.StartDialog(dialogNodeGraph);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
