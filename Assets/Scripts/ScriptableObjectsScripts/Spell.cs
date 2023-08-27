using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spell", order = 0)]
public class Spell : ScriptableObject
{
    public float spellDamage;
    public float spellDuration; // Only use this if the spell is supposed to stay for a limited time
    public float spellCoolDown;
    public float spellRange;
    public float amountOfObjects;
    public float[] specialEffectDuration; //If u select multiple u need to put them in the same order as they are in the list
    public float[] specialEffectDamage;
    public bool homing;
    public GameObject spellObjectPrefab;
    public GameObject target;
    public LayerMask spellLayerMask;
    public TypeOfSpell typeOfSpell;
    public SpecialEffect specialEffects;
}
