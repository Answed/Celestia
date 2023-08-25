using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quicksand : MonoBehaviour, Spell
{
    [SerializeField] private float spellDamage;
    [SerializeField] private float spellCooldown;
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private LayerMask spellLayerMask;

    private float nextCast;
    private bool displayArea;
    private GameObject spellArea;

    public void CastSpell(Transform projectileSpawnPoint, Transform projectileDirection)
    {
        if (!displayArea)
        {
            spellArea = Instantiate(spellPrefab);
            displayArea = true;
        }
        DisplaySpellArea(projectileSpawnPoint, projectileDirection);
    }

    public void ResetSpell()
    {
        nextCast = 0;
        displayArea = false;
    }

    private void DisplaySpellArea(Transform projectileSpawnPoint, Transform projectileDirection)
    {
        RaycastHit hit;
        Debug.DrawRay(projectileSpawnPoint.position, projectileDirection.forward);

        if (Physics.Raycast(projectileSpawnPoint.position, projectileDirection.forward, out hit, spellLayerMask))
        {
            Debug.Log(hit.point);
            if (hit.collider.CompareTag("Ground"))
            {
                spellArea.transform.position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);
            }
        }
    }


}
