using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;
using TMPro;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public SceneData sceneData;
    public TextMeshProUGUI Objective;
    public List<string> foundClues = new List<string>();
    public int currentSceneIndex = 0;
    private void Awake() {
        Instance = this;
    }
    private void Start() {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void OnClueFound(string clueID, GameObject clueObject)
    {
        foundClues.Add(clueID);
        clueObject.SetActive(true);
        Objective.text = "Objective - Find The Clues Scattered Across the House(" + foundClues.Count + "/3)";
    }
    private void Update() {
        if (foundClues.Count == sceneData.Clues.Length) {
            Objective.text = "Objective - Head to The Door";
        }
    }
    
}
