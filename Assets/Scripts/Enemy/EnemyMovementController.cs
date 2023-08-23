using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyState
{
    Idle,
    Move,
    Follow,
    Invisible,
    Chase,
    RegularAttack1, // There will be more 
    SpecialAttack1, // Want to make the framework work first
}

public class EnemyMovementController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float moveSpeed;
    [SerializeField] private float searchingRadius;
    [SerializeField] private float maxChaseTime;
    [SerializeField] private float chaseProbability; // The higher the value the higher the probability. Shouldn't be higher than 50% -> 0.5
    [SerializeField] private float chaseDelay;
    [SerializeField] private float orbitPointUpdateDelay;
    [SerializeField] private float orbitDistance;
    [SerializeField] private float invisibleProbability;
    [SerializeField] private float invisibleDelay;
    [SerializeField] private float maxInvisibleTime;
    [SerializeField] private float teleportProbability;
    [SerializeField] private float teleportDelay;

    private NavMeshAgent agent;
    private bool isRotating; //Is needed to prevent multiple callings of the method
    private bool isMoving; // So he doesen't move and looks around at the same time

    private float currentMaxChaseTime;
    private float chaseTime;
    private float nextChaseUpdate;
    private float nextInvisibleUpdate;
    private float currentMaxInvisTime;
    private float invisTime;
    private float nextPointUpdate;
    private float nextTeleport;

    private int currentDegree;

    private Dictionary<EnemyState, Action> stateHandlers;
    private EnemyState currentState;
    private Transform lastPlayerPosition;
    private EnemyFieldOfView enemyView;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyView = GetComponent<EnemyFieldOfView>();
        agent.speed = moveSpeed;
        stateHandlers = new Dictionary<EnemyState, Action>
        {
            {EnemyState.Idle, UpdateIdleState},
            {EnemyState.Move, UpdateMoveState},
            {EnemyState.Follow, UpdateFollowState},
            {EnemyState.Invisible, UpdateInvisibleState},
            {EnemyState.Chase, UpdateChaseState},
            {EnemyState.RegularAttack1, UpdateRegularAttack1State},
            {EnemyState.SpecialAttack1, UpdateSpecialAttack1State},
        };
        currentState = new EnemyState();
        currentState = EnemyState.Move;
        lastPlayerPosition = transform; //Player was never found so the start point is josh position
    }

    // Update is called once per frame
    void Update()
    {
        if (stateHandlers.TryGetValue(currentState, out Action handler))
            handler.Invoke();

        if (enemyView.playerFound)
        {
            StopAllCoroutines();
            isMoving = true;
        }
    }
    private void UpdateIdleState()
    {
        //Not sure what to implement here
    }
    private void UpdateMoveState()
    {
        agent.speed = moveSpeed;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!isRotating)
            {
                isRotating = true;
                isMoving = false;
                StartCoroutine(LookAround());
            }
            if (isMoving)
            {
                Vector3 point;
                if (RandomPointOnNavMesh(lastPlayerPosition.position, searchingRadius, out point))
                {
                    agent.SetDestination(point);
                    isRotating = false;
                }
                float teleportToNextPoint = UnityEngine.Random.Range(0.0f, 1.0f);

                if (teleportToNextPoint < teleportProbability && teleportDelay >= nextTeleport)
                {
                    nextTeleport = Time.time + teleportDelay;
                    transform.position = point;
                }
            }
        }
        if (enemyView.playerFound) currentState = EnemyState.Follow;
        else lastPlayerPosition = CurrentTarget();
    }
    private void UpdateFollowState()
    {
        agent.speed = moveSpeed / 2; //Will be changed later on. Just for testing right now
        agent.SetDestination(enemyView.player.position);

        if (!enemyView.playerFound)
        {
            currentState = EnemyState.Move;
            lastPlayerPosition = CurrentTarget();
        }

        float switchToChase = UnityEngine.Random.Range(0.0f, 1.0f);
        float switchToInvisible = UnityEngine.Random.Range(0.0f, 2.0f);

        if (switchToChase < chaseProbability && Time.time >= nextChaseUpdate)
        {
            currentMaxChaseTime = UnityEngine.Random.Range(0, maxChaseTime);
            chaseTime = 0;
            nextChaseUpdate = Time.time + chaseDelay;
            currentState = EnemyState.Chase;
        }
        if (switchToInvisible < invisibleProbability && Time.time >= nextInvisibleUpdate)
        {
            nextInvisibleUpdate = Time.time + invisibleDelay;
            currentMaxInvisTime = UnityEngine.Random.Range(0, maxInvisibleTime);
            invisTime = 0;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            currentState = EnemyState.Invisible;
        }
    }
    private void UpdateInvisibleState()
    {
        agent.SetDestination(enemyView.player.position);
        if (!enemyView.playerFound || invisTime >= currentMaxInvisTime)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            lastPlayerPosition = CurrentTarget();
            currentState = EnemyState.Move;
        }
        invisTime += Time.deltaTime;
    }
    private void UpdateChaseState()
    {
        agent.speed = moveSpeed * 2;
        agent.SetDestination(enemyView.player.position);

        if (chaseTime >= currentMaxChaseTime)
        {
            nextChaseUpdate = Time.time + chaseDelay;
            currentState = EnemyState.Follow;
        }
        else chaseTime += Time.deltaTime;
    }
    private void UpdateRegularAttack1State()
    {
        //Not sure what to implement here
    }
    private void UpdateSpecialAttack1State()
    {
        //Not sure what to implement here
    }
    private bool RandomPointOnNavMesh(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }
    IEnumerator LookAround()
    {
        float rotationDelay = 0.01f;
        float rotationProgress = 0;
        WaitForSeconds delay = new WaitForSeconds(rotationDelay);

        for (int i = 0; i <= 2; i++) // Rotates around the clokc every second time
        {
            Quaternion startRotation = transform.rotation;
            if (i % 2 == 0)
            {
                Quaternion targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + 90, 0);
                while (rotationProgress < 1)
                {
                    rotationProgress += Time.deltaTime * 2;
                    transform.rotation = Quaternion.Lerp(startRotation, targetRotation, rotationProgress);

                    yield return delay;
                }
            }
            else
            {
                Quaternion targetRotation = Quaternion.Euler(0, transform.eulerAngles.y - 180, 0);
                while (rotationProgress < 1)
                {
                    rotationProgress += Time.deltaTime * 2;
                    transform.rotation = Quaternion.Lerp(startRotation, targetRotation, rotationProgress);

                    yield return delay;
                }
            }
            rotationProgress = 0;
        }

        isMoving = true;

        if (enemyView.playerFound)
            currentState = EnemyState.Follow;
        else currentState = EnemyState.Move;
    }
    private Transform CurrentTarget() // Ensures that the code doesen't break in the beginning
    {
        if (enemyView.player != null)
            return enemyView.player;
        return transform;
    }

    private Vector3 nextPointOnCircle(int currentDegree) // Will be used to calculate the Next point on the orbit around the player
    {
        return new Vector3(enemyView.player.position.x + orbitDistance * (float)Math.Cos(currentDegree * Math.PI / 180F), enemyView.player.position.y,
            enemyView.player.position.z + orbitDistance * (float)Math.Sin(currentDegree * Math.PI / 180F));
    }

    private int StartDegree()
    {
        float skalarproduct = Vector3.Dot(enemyView.player.forward, transform.position);
        return (int)Math.Acos(skalarproduct / enemyView.player.forward.magnitude * transform.position.magnitude);
    }
}
