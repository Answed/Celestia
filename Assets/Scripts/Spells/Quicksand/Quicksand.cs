using System.Collections.Generic;
using UnityEngine;

public class Quicksand : MonoBehaviour
{
    [SerializeField] private float spellTime;
    [SerializeField] private float spellCooldown;
    [SerializeField] private GameObject spellAreaPrefab;
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
                spellArea = Instantiate(spellAreaPrefab);
                displayArea = true;
            }
            if (releasedSpell)
            {
                displayArea = false;
                nextCast = Time.time + spellCooldown + spellTime;
                PlaceSpell();
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

    private void PlaceSpell()
    {
        Vector3 spellPosition = spellArea.transform.position;
        Destroy(spellArea);
        GameObject spell =  Instantiate(spellPrefab, spellPosition, Quaternion.identity);
        spell.GetComponent<QuicksandEffect>().spellDuration = spellTime;
    }

}
