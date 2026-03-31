using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "Scriptable Objects/SceneData")]
public class SceneData : ScriptableObject
{
    public GameObject[] Clues;
    public HashSet<string> foundClues = new HashSet<string>();
    public string ObjectiveText;
    public string Cluestart;
    public string Clueend;
    private void Awake() {
    }
}   
