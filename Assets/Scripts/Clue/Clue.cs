using UnityEngine;
using UnityEngine.Playables;

public class Clue : MonoBehaviour
{
    public string clueName;
    public GameObject clue;
    public PlayableAsset playableAsset;
    public bool isFound = false;

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player") && PlayerInputHandler.Instance.InteractInput) {
            if (GameManager.Instance.sceneData.foundClues.Add(clueName))  
            {
                isFound = true;
                GameManager.Instance.OnClueFound(clueName, clue, playableAsset);
                Debug.Log($"Clue {clueName} found!");
            }
        }
    }
}
