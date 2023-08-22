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

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        projectileSpawnPoint = GameObject.Find("CombatLookAt").GetComponent<Transform>();
        projectileDirection = GameObject.Find("PlayerCam").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputHandler.attack1 == 1 && nextAttack < Time.time)
        {
            nextAttack = Time.time + attackDelay;
            BasicAttack();
        }
    }

    private void BasicAttack()
    {
        GameObject currentProjectile = Instantiate(magic.Attack1, projectileSpawnPoint.position, Quaternion.identity);
        Debug.Log(projectileSpawnPoint.forward);
        currentProjectile.GetComponent<Projectile>().ThrowProjectile(projectileDirection.forward);

    }
}
