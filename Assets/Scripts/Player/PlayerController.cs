using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.VFX;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Rigidbody Rbody;
    PlayerInputHandler InputHandler;
    [SerializeField] PlayerData Data;
    [SerializeField] CinemachineCamera FollowCam;
    public Transform GroundCheck;
    public float GroundCheckRadius;
    public LayerMask GroundLayer;
    public VisualEffectAsset Scan;
    [SerializeField] private float maxScanDistance = 10f;
    [SerializeField] private float expandDuration = 3f;
    Collider[] ScannedObjects;
    public LayerMask targetLayerMask;
    float currentRadius = 0f;
    public Material ScanMat;
    bool TouchedGround()
    {
        return Physics.CheckSphere(GroundCheck.position, GroundCheckRadius, GroundLayer);
    }

    void Start()
    {
        Rbody = GetComponent<Rigidbody>();
        InputHandler = GetComponent<PlayerInputHandler>();
    }
    void Update()
    {

        if (TouchedGround() && !Data.isGrounded)
        {
            Data.isGrounded = true;
        }
        else if (!TouchedGround() && Data.isGrounded)
        {
            Data.isGrounded = false;
        }

        if (InputHandler.ScanInput)
        {
            StartExpandingScan();
        }

    }
    void FixedUpdate()
    {
        HandleMovement();
        LimitSpeed();
        ApplyDrag();
    }

    private void HandleMovement()
    {
        Vector2 input = InputHandler.MoveInput;
        if (input.sqrMagnitude < 0.01f) return;

        Vector3 camForward = FollowCam.transform.forward;
        Vector3 camRight = FollowCam.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camForward * input.y + camRight * input.x;

        float multiplier = Data.isGrounded ? 1f : Data.AirMoveSpeedMultiplier;

        Rbody.AddForce(moveDir * Data.MoveSpeed * 10f * multiplier, ForceMode.Force);

        Quaternion targetRotation = Quaternion.LookRotation(moveDir);
        Rbody.MoveRotation(
            Quaternion.Slerp(Rbody.rotation, targetRotation, 10f * Time.fixedDeltaTime)
        );
    }

    private void LimitSpeed()
    {
        Vector3 flatVelocity = new Vector3(Rbody.linearVelocity.x, 0f, Rbody.linearVelocity.z);

        if (flatVelocity.magnitude <= Data.MoveSpeed) return;

        Vector3 limited = flatVelocity.normalized * Data.MoveSpeed;

        Rbody.linearVelocity = new Vector3(limited.x, Rbody.linearVelocity.y, limited.z);
    }

    private void ApplyDrag()
    {
        Rbody.linearDamping = Data.isGrounded ? Data.GroundDrag : Data.AirDrag;
    }
    public void StartExpandingScan()
    {
        StartCoroutine(ExpandingScanCoroutine());
    }

    private IEnumerator ExpandingScanCoroutine()
    {
        float elapsed = 0f;


        while (elapsed < expandDuration)
        {
            elapsed += Time.deltaTime;
            currentRadius = Mathf.Lerp(0f, maxScanDistance, elapsed / expandDuration); // Smooth expansion [web:19]

            ScannedObjects = Physics.OverlapSphere(transform.position, currentRadius, targetLayerMask);
            foreach (Collider col in ScannedObjects)
            {
                col.GetComponent<MeshRenderer>().material = ScanMat;
            }

            yield return null;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, currentRadius);
    }

}
