using Unity.Cinemachine;
using UnityEngine;

public class UITransitionScript : MonoBehaviour
{
   public CinemachineCamera CurrentCam;

    void Start()
    {
        CurrentCam.Priority++;
    }

    public void SwitchToCam(CinemachineCamera target)
    {
        CurrentCam.Priority--;
        CurrentCam = target;
        CurrentCam.Priority++;
    }

}
