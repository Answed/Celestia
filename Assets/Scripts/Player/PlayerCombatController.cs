using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private MagicElement magic;
    
    private InputHandler inputHandler;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputHandler.attack1 == 1)
            BasicAttack();
    }

    private void BasicAttack()
    {
        GameObject currentProjectile = Instantiate(magic.Attack1, transform.position, Quaternion.identity);
        currentProjectile.GetComponent<Projectile>().Throw(transform.forward);
    }
}
