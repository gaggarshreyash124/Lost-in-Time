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

    public bool KeycardsFound = false;
    public bool GlassesFound = false;
    public PlayerData playerData;
    public GameObject InteractButton;
    public GameObject AbilityButton;
    private void Awake()
    {
        Instance = this;
        playableDirector = GetComponent<PlayableDirector>();
    }
    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex <= 4)
        {
            playerData.GlassesFound = false;
        }
        else if (currentSceneIndex > 4)
        {
            playerData.GlassesFound = true;
        }
    }

    public void OnClueFound(string clueID, GameObject clueObject, PlayableAsset playableAsset, AudioClip audioClip)
    {
        foundClues.Add(clueID);
        clueObject.SetActive(true);
        Objective.text = sceneData.Cluestart + foundClues.Count + sceneData.Clueend;
        if (playableAsset != null)
        {
            playableDirector.Play(playableAsset);
        }
    }
    public void OnPickableFound(string clueID, GameObject clueObject, PlayableAsset playableAsset, AudioClip audioClip)
    {
        foundClues.Add(clueID);
        clueObject.SetActive(true);
        Objective.text = "Objective - " + sceneData.ObjectiveText;
        if (playableAsset != null)
        {
            playableDirector.Play(playableAsset);
        }
        if (audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
    private void Update()
    {
        if (foundClues.Count == sceneData.Clues.Length)
        {
            if (playableDirector.state != PlayState.Playing && !audioPlayed)
            {
                if (SceneManager.GetActiveScene().buildIndex == 4)
                {
                    Objective.text = "Objective - Use Glasses near Body to find out what happened";
                    if (playerData.PastPlayed && playableDirector.state != PlayState.Playing)
                    {
                        Objective.text = "Objective - Head to the door";
                        audioSource.PlayOneShot(audioClip);
                        audioPlayed = true;
                    }
                }
                else if (SceneManager.GetActiveScene().buildIndex == 2)
                {
                    Objective.text = "Objective - Head to the door ";
                    audioSource.PlayOneShot(audioClip);
                    audioPlayed = true;
                }

            }
        }
        if (playerData != null)
        {
            if (GlassesFound && !playerData.GlassesFound)
            {
                playerData.GlassesFound = true;
            }
        }

    }

}
