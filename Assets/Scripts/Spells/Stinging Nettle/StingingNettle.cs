using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class StingingNettle : MonoBehaviour, Spell
{
    [SerializeField] private float spellTime;
    [SerializeField] private float amountOfFlowers;
    [SerializeField] private float spellArea;
    [SerializeField] private float spellCooldown;
    [SerializeField] private GameObject spellPrefab;

    private float nextCast;
    public void CastSpell(Transform projectileSpawnPoint, Transform projectileDirection, bool spellReleased)
    {
        if(nextCast <= Time.time)
        {
            nextCast = Time.time + spellCooldown + spellTime;
            Transform playerPosition = GameObject.Find("Player").GetComponent<Transform>();

            for(int i = 0;i < amountOfFlowers;i++)
            {
                Vector3 point;
                if(RandomPointOnNavMesh(playerPosition.position, spellArea, out point))
                {
                    GameObject flower = Instantiate(spellPrefab, point, Quaternion.identity);
                    flower.GetComponent<StiningNettleEffect>().spellDuration = spellTime;
                }
            }
        }
    }

    public void ResetSpell()
    {
        nextCast = 0;
    }

    private bool RandomPointOnNavMesh(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }
}
