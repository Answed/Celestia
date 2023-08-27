using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuicksandEffect : MonoBehaviour
{
    public float spellDuration;
    private List<GameObject> enemies;

    private void Start()
    {
        enemies = new List<GameObject>(); 
        StartCoroutine(DestroyAfterTime());
        Debug.Log(spellDuration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log(other);
            other.GetComponent<EnemyMovementController>().currentState = EnemyState.Trapped;
            enemies.Add(other.gameObject);
        }
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(spellDuration);

        foreach(GameObject enemy in enemies)
        {
            if(enemy != null)
                enemy.GetComponent<EnemyMovementController>().currentState = EnemyState.Move;
        }

        Destroy(gameObject);
    }
}
