using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Scriptable Objects/Items")]
public class Items : ScriptableObject
{
    [TitleGroup("Item Information")]
    [Header("Item Sprite")]
    [HideLabel]
    public Sprite ItemSprite;

    [Header("Item Name")]
    [HideLabel]
    public string ItemName;

    [Header("Item Type")]
    [HideLabel]
    public ItemType ItemType;
}


public enum ItemType
{
    Clue,
    Equipment,
    Random
}