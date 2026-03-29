using UnityEngine;
using System;
using System.Collections.Generic;

public class Clue : MonoBehaviour
{
    public string clueName;
    public GameObject clue;
    
    public bool isFound = false;

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player") && PlayerInputHandler.Instance.InteractInput) {
            if (GameManager.Instance.sceneData.foundClues.Add(clueName))  
            {
                isFound = true;
                GameManager.Instance.OnClueFound(clueName, clue);
                Debug.Log($"Clue {clueName} found!");
            }
        }
    }
}
