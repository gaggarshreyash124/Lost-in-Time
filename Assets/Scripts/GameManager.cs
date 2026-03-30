using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Playables;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public SceneData sceneData;
    public TextMeshProUGUI Objective;
    public List<string> foundClues = new List<string>();
    public int currentSceneIndex = 0;
    private PlayableDirector playableDirector;
    public AudioSource audioSource;
    public AudioClip audioClip;
    bool audioPlayed = false;
    public bool Detected;
    public float DetectionTime;
    public float CaughtTime = 5f;
    public GameObject CaughtPanel;
    bool Caught = false;
    public GameObject Warning;

    [HideInInspector]
    public bool KeycardsFound = false;
    private void Awake() {
        Instance = this;
        playableDirector = GetComponent<PlayableDirector>();
    }
    private void Start() {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void OnClueFound(string clueID, GameObject clueObject,PlayableAsset playableAsset)
    {
        foundClues.Add(clueID);
        clueObject.SetActive(true);
        Objective.text = sceneData.ObjectiveText;
        if (playableAsset != null)
        {
            playableDirector.Play(playableAsset);
        }
    }
    private void LateUpdate() {
        if (foundClues.Count == sceneData.Clues.Length) {
            if (playableDirector.state != PlayState.Playing && !audioPlayed)
            {
                Objective.text = "Objectivew - Head to The Door";
                audioSource.PlayOneShot(audioClip);
                audioPlayed = true;
            }
        }
    }

    private void Update() {
        if (Detected && !Caught && Time.time - DetectionTime > CaughtTime) {
            Caught = true;
            CaughtPanel.SetActive(true);
        }
        else if (Detected && !Caught) {
            Warning.SetActive(true);
        }
        else {
            Warning.SetActive(false);
        }
    }
    
}
