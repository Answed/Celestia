using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StiningNettleEffect : MonoBehaviour
{
    public float spellDuration;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfterTime());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            other.GetComponent<EnemyController>().Poisend();
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(spellDuration);
        Destroy(gameObject);
    }
}
