using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class CutsceneManagerScript : MonoBehaviour
{
    PlayableDirector playableAsset;
    
    void Start()
    {
        playableAsset = GetComponent<PlayableDirector>();
    }
    public void SkipCutscene()
    {
        playableAsset.time = 1600;
        playableAsset.Evaluate();
        Debug.Log("Cutscene skipped!");
    }
    public void EndCutscene()
    {
        SceneManager.LoadScene(2);
        Debug.Log("Cutscene ended!");
    }
}
