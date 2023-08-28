using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTemplate : MonoBehaviour 
{
    protected Spell spell;

    public void SetSpellData(Spell spell)
    {
        this.spell = spell;
    }

    public void ApplyEffects()
    {
        string[] appliedEffects = spell.specialEffects.ToString().Split(';');
        //Need to figuer out how we want the effects to work
    }


}
