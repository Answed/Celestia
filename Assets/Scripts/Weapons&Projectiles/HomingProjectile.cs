using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    private Spell currentSpell;
    private bool followTarget;
    

    // Update is called once per frame
    void Update()
    {
        if (followTarget)
        {
            transform.Translate(currentSpell.target.transform.position);
        }
    }

    public void SetTarget(Spell spell)
    {
        currentSpell = spell;
        followTarget = true;
    }
}
