using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;
    public Transform Raypoint;
    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    public Transform[] PatrolPoints;
    public int destPoint = 0;
    public bool iswaiting = false;
    public float waitTime = 2f;
    public float threshold = 0.5f;
    private NavMeshAgent agent;

    [Header("Angle Limits")]
    public float minAngle = -45f;
    public float maxAngle = 45f;
    public bool startMovingRight = true;

    [Header("Rotation Settings")]
    public float rotationSpeed = 100f;
    float currentAngle;
    int direction;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GotoNextPoint();
        StartCoroutine(FOVRoutine());

        currentAngle = minAngle;
        direction = startMovingRight ? 1 : -1;

        ApplyRotation();
    }
    void ApplyRotation()
    {
        Vector3 rot = Raypoint.transform.localEulerAngles;
        rot.y = currentAngle;
        Raypoint.transform.localEulerAngles = rot;
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
            GameManager.Instance.Detected = canSeePlayer;
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(Raypoint.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                    GameManager.Instance.DetectionTime = Time.time;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }


    void GotoNextPoint()
    {
        if (PatrolPoints.Length == 0) return;
        agent.destination = PatrolPoints[destPoint].position;
        destPoint = (destPoint + 1) % PatrolPoints.Length;
    }

    void Update()
    {
       currentAngle += direction * rotationSpeed * Time.deltaTime;

        if(currentAngle >= maxAngle)
        {
            currentAngle = maxAngle;
            direction = -1;
        }
        else if(currentAngle <= minAngle)
        {
            currentAngle = minAngle;
            direction = 1;
        }

        ApplyRotation();
    }
    void Patrol()
    {
        if (iswaiting) return;
        
        if (!agent.pathPending && agent.remainingDistance < threshold)
        {
            StartCoroutine(WaitThenPatrol());
        }
    }

    IEnumerator WaitThenPatrol()
    {
        iswaiting = true;
        agent.ResetPath();  // Stop precisely at point
        yield return new WaitForSeconds(waitTime);
        GotoNextPoint();
        iswaiting = false;
    }

    
}
