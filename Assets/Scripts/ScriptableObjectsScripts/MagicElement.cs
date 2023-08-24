using UnityEngine;

[CreateAssetMenu(fileName = "MagicElement", menuName ="Magic Element", order = 0)]
public class MagicElement : ScriptableObject
{
    public Spell Attack1;
    public Spell Attack2;
    public Spell Attack3;
    public Spell Ultimate;
}
