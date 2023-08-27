using System;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public enum TypeOfSpell
{
    Projectile,
    SpawnsAroundPlayer,
    AOE,
    SelfCast,
    OtherCast
}

[Flags]
public enum SpecialEffect
{
    burning = 1,
    bleeding = 2,
    poisend = 4,
    stunt = 8,
    trapped = 16
}

public class SpellCaster : MonoBehaviour
{
    [SerializeField] private GameObject areaDisplay;
    
    private Spell currentSpell;
    private SpecialEffect specialEffects;
    private bool displayArea;

    public void CastSpell(Transform projectileSpawnPoint, Transform projectileDirection,Spell selectedSpell, bool spellReleased)
    {
        currentSpell = selectedSpell;

        if(currentSpell.nextCast <= Time.time)
        {
            currentSpell.nextCast = Time.time + currentSpell.spellCoolDown + currentSpell.spellDuration;
            switch (currentSpell.typeOfSpell)
            {
                case TypeOfSpell.Projectile:
                    Projectile(projectileSpawnPoint, projectileDirection);
                    break;
                case TypeOfSpell.SpawnsAroundPlayer:
                    SpawnAroundPlayer();
                    break;
                case TypeOfSpell.AOE:
                    AOE(spellReleased, projectileSpawnPoint, projectileDirection);
                    break;
                case TypeOfSpell.SelfCast:
                    break;
                case TypeOfSpell.OtherCast:
                    break;
            }
        }
    }
    
    private void Projectile(Transform projectileSpawnPoint, Transform projectileDirection)
    {
            GameObject currentProjectile = Instantiate(currentSpell.spellObjectPrefab, projectileSpawnPoint.position, Quaternion.identity);
            currentProjectile.GetComponent<Projectile>().ThrowProjectile(projectileDirection.forward, currentSpell.spellDamage);
    }
    private void SpawnAroundPlayer()
    {
        Transform playerPosition = GameObject.Find("Player").GetComponent<Transform>();

        for (int i = 0; i < currentSpell.amountOfObjects; i++)
        {
            Vector3 point;
            if (RandomPointOnNavMesh(playerPosition.position, currentSpell.spellRange, out point))
            {
                GameObject flower = Instantiate(currentSpell.spellObjectPrefab, point, Quaternion.identity);
                // Can be used to apply all the missing values.
            }
        }
    }
    private void AOE(bool releasedSpell, Transform projectileSpawnPoint, Transform projectileDirection)
    {
        if (!displayArea)
        {
            areaDisplay = Instantiate(areaDisplay);
            displayArea = true;
            ChangeRadiousOfSpellArea();
        }
        if (releasedSpell)
        {
            displayArea = false;
            PlaceSpell();
        }
        DisplaySpellArea(projectileSpawnPoint, projectileDirection);
    }

    private void SelfCast()
    {
        //Not a clue how i want to apply the effects to the player Probably with a list or something
    }

    private void OtherCast(Transform projectileSpawnPoint, Transform projectileDirection, GameObject target)
    {
        // Could also Implement a raycast system so the target gets decided when the player uses the spell.
    }

    private void ChangeRadiousOfSpellArea()
    {
        areaDisplay.transform.localScale = new Vector3(currentSpell.spellRange, areaDisplay.transform.localScale.y, currentSpell.spellRange);
        currentSpell.spellObjectPrefab.transform.localScale = new Vector3(currentSpell.spellRange, areaDisplay.transform.localScale.y, currentSpell.spellRange); // Ensures that the actual area has the same size
    }

    private void DisplaySpellArea(Transform projectileSpawnPoint, Transform projectileDirection)
    {
        RaycastHit hit;

        if (Physics.Raycast(projectileSpawnPoint.position, projectileDirection.forward, out hit, currentSpell.spellLayerMask))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                areaDisplay.transform.position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);
            }
        }
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

    private void PlaceSpell()
    {
        Vector3 spellPosition = areaDisplay.transform.position;
        Destroy(areaDisplay);
        GameObject spell = Instantiate(currentSpell.spellObjectPrefab, spellPosition, Quaternion.identity);
        // Can be used to apply all the missing values.
    }

    //Is requierd so mutliple special effects can be used.
    private bool IsValidSpecialEffect(SpecialEffect effect)
    {
        return (specialEffects & effect) != 0;
    }
}
