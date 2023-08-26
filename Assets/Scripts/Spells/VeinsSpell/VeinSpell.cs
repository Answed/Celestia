using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VeinSpell : MonoBehaviour, Spell
{
    [SerializeField] private float spellDamage;
    [SerializeField] private float spellTime;
    [SerializeField] private float amountOfVeins;
    [SerializeField] private float spellArea;
    [SerializeField] private float spellCooldown;
    [SerializeField] private GameObject spellPrefab;

    private float nextCast;
    public void CastSpell(Transform projectileSpawnPoint, Transform projectileDirection, bool spellReleased)
    {
        if (nextCast <= Time.time)
        {
            nextCast = Time.time + spellCooldown + spellTime;
            Transform playerPosition = GameObject.Find("Player").GetComponent<Transform>();

            for (int i = 0; i < amountOfVeins; i++)
            {
                Vector3 point;
                if (RandomPointOnNavMesh(playerPosition.position, spellArea, out point))
                {
                    GameObject vein = Instantiate(spellPrefab, point, Quaternion.identity);
                    vein.GetComponent<Vein>().ApplyValues(spellDamage, spellTime);
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
