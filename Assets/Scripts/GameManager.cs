
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public SceneData sceneData;
    public List<string> foundClues = new List<string>();

    private void Awake() {
        Instance = this;
    }

    public void OnClueFound(string clueID, GameObject clueObject)
    {
        foundClues.Add(clueID);
        clueObject.SetActive(true);
    }
}
