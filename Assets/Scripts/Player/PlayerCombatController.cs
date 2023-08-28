using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private MagicElement magic;

    private InputHandler inputHandler;
    private Transform projectileSpawnPoint;
    private Transform projectileDirection;
    private ElementsManager elementsManager;
    private SpellCaster spellCaster;

    private MagicElement currentElement;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        spellCaster = GetComponent<SpellCaster>();
        projectileSpawnPoint = GameObject.Find("CombatLookAt").GetComponent<Transform>();
        projectileDirection = GameObject.Find("PlayerCam").GetComponent<Transform>();
        elementsManager = GetComponent<ElementsManager>();
        currentElement = elementsManager.elements[0];
    }

    // Update is called once per frame
    void Update()
    {
       if (inputHandler.basicAttack == 1 || inputHandler.basicAttackReleased)
        {
            spellCaster.CastSpell(projectileSpawnPoint, projectileDirection, currentElement.basicAttack, inputHandler.basicAttackReleased);
            inputHandler.basicAttackReleased = false;
        }
        if(inputHandler.spell1 == 1 || inputHandler.spell1Released)
        {
            spellCaster.CastSpell(projectileSpawnPoint, projectileDirection, currentElement.basicAttack, inputHandler.spell1Released);
            inputHandler.spell1Released = false;
        }
        if(inputHandler.spell2 == 1 || inputHandler.spell2Released)
        {
            spellCaster.CastSpell(projectileSpawnPoint, projectileDirection, currentElement.basicAttack, inputHandler.spell2Released);
            inputHandler.spell2Released = false;
        }
        if(inputHandler.ultimate == 1 || inputHandler.ultimateReleased)
        {
            spellCaster.CastSpell(projectileSpawnPoint, projectileDirection, currentElement.basicAttack, inputHandler.ultimateReleased);
            inputHandler.ultimateReleased = false;
        }
    }
}
