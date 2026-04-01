using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float MoveSpeed;
    public bool isGrounded;
    public float AirDrag;
    public float GroundDrag;
    public float AirMoveSpeedMultiplier;
    public bool GlassesFound;
    public bool PastPlayed;
}
