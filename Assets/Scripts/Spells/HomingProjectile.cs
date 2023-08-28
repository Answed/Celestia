using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : SpellTemplate
{
    private bool followTarget;
    

    // Update is called once per frame
    void Update()
    {
        if(spell != null)
            followTarget = true;

        if (followTarget)
        {
            transform.Translate(spell.target.transform.position);
        }
    }

}
