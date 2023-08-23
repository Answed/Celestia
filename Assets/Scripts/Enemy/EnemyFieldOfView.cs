using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldOfView : MonoBehaviour
{
    public Transform player;
    public bool playerFound;
    public float fieldOfView;
    public float viewingDistance;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FieldOfViewCheckDelay());
    }

    IEnumerator FieldOfViewCheckDelay()
    {
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] findTarget = Physics.OverlapSphere(transform.position, viewingDistance, targetMask);

        if (findTarget.Length != 0)
        {
            player = findTarget[0].transform;
            Vector3 directtionToTarget = (player.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directtionToTarget) < fieldOfView / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, player.position);

                if (!Physics.Raycast(transform.position, directtionToTarget, distanceToTarget, obstructionMask))
                    playerFound = true;
            }
            else playerFound = false;
        }
    }
}
