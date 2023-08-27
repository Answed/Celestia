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
            element.basicAttack.nextCast = 0;
            element.spell.nextCast = 0;
            element.spell2.nextCast = 0;
            element.ultimate.nextCast = 0;
        }
    }
}
