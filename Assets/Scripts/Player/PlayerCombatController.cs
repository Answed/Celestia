using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private MagicElement magic;
    [SerializeField] private float attackDelay;

    private float nextAttack;
    private InputHandler inputHandler;
    private Transform projectileSpawnPoint;
    private Transform projectileDirection;
    private ElementsManager elementsManager;

    private Element currentElement;

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
        if (inputHandler.attack1 == 1 && nextAttack < Time.time)
        {
            nextAttack = Time.time + attackDelay;
            currentElement.basicAttack.GetComponent<Spell>().CastSpell(projectileSpawnPoint, projectileDirection);
        }
    }
}
