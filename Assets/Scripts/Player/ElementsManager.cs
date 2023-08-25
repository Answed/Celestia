using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsManager : MonoBehaviour
{

    public MagicElement[] elements;
    // Start is called before the first frame update
    private void Start()
    {
        ResetAllSpells(elements);
    }

    private void ResetAllSpells(MagicElement[] elements)
    {
        foreach (MagicElement element in elements)
        {
            element.basicAttack.GetComponent<Spell>().ResetSpell();
            element.spell.GetComponent<Spell>().ResetSpell();
            element.spell2.GetComponent<Spell>().ResetSpell();
            element.ultimate.GetComponent<Spell>().ResetSpell();
        }
    }
}
