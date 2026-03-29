using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.VFX;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    Rigidbody Rbody;
    [SerializeField] PlayerData Data;
    [SerializeField] CinemachineCamera FollowCam;
    public Transform GroundCheck;
    public float GroundCheckRadius;
    public LayerMask GroundLayer;
    public VisualEffectAsset Scan;
    [SerializeField] private float maxScanDistance = 10f;
    [SerializeField] private float expandDuration = 1f;
    Collider[] ScannedObjects;
    public LayerMask targetLayerMask;
    public LayerMask InteractableLayerMask;
    float currentRadius = 0f;
    public Material ScanMat;
    float counter;
    [Range(0f, 1f)]
    public float ScanEffectIntensity = 0.35f;

    bool TouchedGround()
    {
        return Physics.CheckSphere(GroundCheck.position, GroundCheckRadius, GroundLayer);
    }

    void Start()
    {
        Rbody = GetComponent<Rigidbody>();
        
    }
    void Update()
    {
        counter += Time.deltaTime;
        if (TouchedGround() && !Data.isGrounded)
        {
            Data.isGrounded = true;
        }
        else if (!TouchedGround() && Data.isGrounded)
        {
            Data.isGrounded = false;
        }

        if (PlayerInputHandler.Instance.ScanInput)
        {
            counter = 0f;
            StartExpandingScan();
        }
        if (PlayerInputHandler.Instance.InteractInput)
        {
            CheckForDoors();
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
        Vector2 input = PlayerInputHandler.Instance.MoveInput;
        if (input.sqrMagnitude < 0.01f) return;

        Vector3 camForward = FollowCam.transform.forward;
        Vector3 camRight = FollowCam.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camForward * input.y + camRight * input.x;

        float multiplier = Data.isGrounded ? 1f : Data.AirMoveSpeedMultiplier;

        Rbody.AddForce(10f * Data.MoveSpeed * multiplier * moveDir, ForceMode.Force);

        Quaternion targetRotation = Quaternion.LookRotation(moveDir);
        Rbody.MoveRotation(
            Quaternion.Slerp(Rbody.rotation, targetRotation, 10f * Time.fixedDeltaTime)
        );
    }

    private void LimitSpeed()
    {
        Vector3 flatVelocity = new(Rbody.linearVelocity.x, 0f, Rbody.linearVelocity.z);

        if (flatVelocity.magnitude <= Data.MoveSpeed) return;

        Vector3 limited = flatVelocity.normalized * Data.MoveSpeed;

        Rbody.linearVelocity = new(limited.x, Rbody.linearVelocity.y, limited.z);
    }

    private void ApplyDrag()
    {
        Rbody.linearDamping = Data.isGrounded ? Data.GroundDrag : Data.AirDrag;
    }
    public void StartExpandingScan()
    {
        PlayerInputHandler.Instance.ScanInput = false;
        StartCoroutine(ExpandingScanCoroutine());
    }
    public void CheckForDoors()
    {
        Physics.Raycast(FollowCam.transform.position, FollowCam.transform.forward, out RaycastHit hit, 3f, InteractableLayerMask);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Door"))
            {
                if (hit.collider.transform.rotation.y == 0f)
                {
                    hit.collider.transform.Rotate(0f, -90f, 0f);
                }
                else
                {
                    hit.collider.transform.Rotate(0f, 90f, 0f);
                }
                Debug.Log("Door Checked");
            }
            
        }
    }
    private IEnumerator ExpandingScanCoroutine()
    {
        float elapsed = 0f;

        while (elapsed < expandDuration)
        {
            elapsed += Time.deltaTime;
            currentRadius = Mathf.Lerp(0f, maxScanDistance, elapsed / expandDuration);

            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, currentRadius, ScannedObjects, targetLayerMask);
            for (int i = 0; i < hitCount; i++)
            {
                ScannedObjects[i].GetComponent<MeshRenderer>().material = ScanMat;
            }

            yield return null;
        }
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, currentRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(FollowCam.transform.position, FollowCam.transform.forward * 3f);
    }

}
