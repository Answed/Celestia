using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpell : MonoBehaviour,Spell 
{
    [SerializeField] private float spellDamage;
    [SerializeField] private float spellCooldown;
    [SerializeField] private GameObject spellPrefab;

    private float nextCast;

    public void CastSpell(Transform projectileSpawnPoint, Transform projectileDirection, bool spellReleased = false)
    {
        if (nextCast <= Time.time)
        {
            nextCast = Time.time + spellCooldown;
            GameObject currentProjectile = Instantiate(spellPrefab, projectileSpawnPoint.position, Quaternion.identity);
            currentProjectile.GetComponent<Projectile>().ThrowProjectile(projectileDirection.forward);
        }
    }

    public void ResetSpell()
    {
        nextCast = 0;
    }

}
