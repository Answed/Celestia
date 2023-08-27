using UnityEngine;

[CreateAssetMenu(fileName = "MagicElement", menuName ="Magic Element", order = 1)]
public class MagicElement : ScriptableObject
{
    public Spell basicAttack;
    public Spell spell;
    public Spell spell2;
    public Spell ultimate;
}
