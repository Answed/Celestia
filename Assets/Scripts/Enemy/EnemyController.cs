using System.Collections;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float poisenResistence;

    private float health;   


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0) Destroy(gameObject);
        Debug.Log(health);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
            health -= 2;
    }

    public void Poisend()
    {
        StartCoroutine(PoisenDamageOverTime());
    }

    IEnumerator PoisenDamageOverTime()
    {
        WaitForSeconds delay = new WaitForSeconds(1);

        for (int i = 0; i < 5 - poisenResistence; i++)
        {
            health -= maxHealth * 0.05f;
            yield return delay;
        }
    }
}
