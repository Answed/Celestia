using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quicksand : MonoBehaviour, Spell
{
    [SerializeField] private float spellDamage;
    [SerializeField] private float spellTime;
    [SerializeField] private float spellCooldown;
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private LayerMask spellLayerMask;

    private float nextCast;
    private bool displayArea;
    private GameObject spellArea;
    private List<GameObject> enemiesInCollider;

    public void CastSpell(Transform projectileSpawnPoint, Transform projectileDirection, bool releasedSpell)
    {
        if(nextCast <= Time.time)
        {
            if (!displayArea)
            {
                spellArea = Instantiate(spellPrefab);
                displayArea = true;
            }
            if (releasedSpell)
            {
                displayArea = false;
                nextCast = Time.time + spellCooldown;
                StartCoroutine(DestroyAfterTime());
            }
            DisplaySpellArea(projectileSpawnPoint, projectileDirection);
        }
    }

    public void ResetSpell()
    {
        nextCast = 0;
        displayArea = false;
    }

    private void DisplaySpellArea(Transform projectileSpawnPoint, Transform projectileDirection)
    {
        RaycastHit hit;

        if (Physics.Raycast(projectileSpawnPoint.position, projectileDirection.forward, out hit, spellLayerMask))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                spellArea.transform.position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyMovementController>().agent.speed = 0;
            enemiesInCollider.Add(collision.gameObject);
        }
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(spellTime);
        foreach (var e in enemiesInCollider)
        {
            e.GetComponent<EnemyMovementController>().agent.speed = 0;
        }
        Destroy(spellArea);
    }

}
