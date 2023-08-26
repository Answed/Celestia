using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vein : MonoBehaviour
{
    private float spellDamage;
    private float spellDuration;
    private bool enemyTrapped;
    private GameObject trappedEnemy;

    private float currentDamage;

    private void Start()
    {
        StartCoroutine(DestroyAfterTime());
        currentDamage = spellDamage;
    }

    public void ApplyValues(float damage, float duration)
    {
        spellDamage = damage;
        spellDuration = duration;  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !enemyTrapped)
        {
            other.GetComponent<EnemyMovementController>().currentState = EnemyState.Trapped;
            enemyTrapped = true;
            trappedEnemy = other.gameObject;
            StartCoroutine(DamageOverTime());
        }
    }

    IEnumerator DamageOverTime()
    {
        WaitForSeconds delay = new WaitForSeconds(1);

        while (true)
        {
            if (trappedEnemy == null)
            {
                enemyTrapped = false;
                break;
            }

            trappedEnemy.GetComponent<EnemyController>().health -= currentDamage;
            currentDamage += spellDamage * 0.5f;
            yield return delay;
        }
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(spellDuration);
        if(trappedEnemy != null)
            trappedEnemy.GetComponent<EnemyMovementController>().currentState = EnemyState.Move;
        Destroy(gameObject);
    }
}
