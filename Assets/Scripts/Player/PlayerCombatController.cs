using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private MagicElement magic;

    private InputHandler inputHandler;
    private Transform projectileSpawnPoint;
    private Transform projectileDirection;
    private ElementsManager elementsManager;

    private MagicElement currentElement;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        projectileSpawnPoint = GameObject.Find("CombatLookAt").GetComponent<Transform>();
        projectileDirection = GameObject.Find("PlayerCam").GetComponent<Transform>();
        elementsManager = GetComponent<ElementsManager>();
        currentElement = elementsManager.elements[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (inputHandler.basicAttack == 1)
            currentElement.basicAttack.GetComponent<Spell>().CastSpell(projectileSpawnPoint, projectileDirection, inputHandler.basicAttackReleased);
        if(inputHandler.spell1 == 1)
            currentElement.spell.GetComponent<Spell>().CastSpell(projectileSpawnPoint, projectileDirection, inputHandler.spell1Released);
        if(inputHandler.spell2 == 1)
            currentElement.spell2.GetComponent<Spell>().CastSpell(projectileSpawnPoint, projectileDirection, inputHandler.spell1Released);
        if(inputHandler.ultimate == 1)
            currentElement.ultimate.GetComponent<Spell>().CastSpell(projectileSpawnPoint, projectileDirection, inputHandler.ultimateReleased);
    }
}
