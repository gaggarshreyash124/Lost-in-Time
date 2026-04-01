using System;
using UnityEngine;
using UnityEngine.Playables;

public class Clue : MonoBehaviour
{
    public string clueName;
    public GameObject clue;
    public PlayableAsset playableAsset;
    public AudioClip audioClip;
    public bool isFound = false;
    public bool isPickable = false;
    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player") && PlayerInputHandler.Instance.InteractInput) {
            if (isPickable)
            {
                this.gameObject.SetActive(false);
                if (clueName == "Keycard")
                {
                    GameManager.Instance.KeycardsFound = true;
                }
                else if (clueName == "Glasses")
                {
                    GameManager.Instance.GlassesFound = true;
                }
                isFound = true;
                GameManager.Instance.OnPickableFound(clueName, clue, playableAsset, audioClip);
                Debug.Log($"Clue {clueName} found!");
            }
            else if (GameManager.Instance.sceneData.foundClues.Add(clueName))  
            {
                isFound = true;
                GameManager.Instance.OnClueFound(clueName, clue, playableAsset, audioClip);
                Debug.Log($"Clue {clueName} found!");
            }
            
        }
    }
}
