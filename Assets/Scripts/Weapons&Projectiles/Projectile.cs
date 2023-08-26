using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float throwForce;
    [SerializeField] private float destroyDelay;
    public float damage;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ThrowProjectile(Vector3 direction, float dm)
    {
        rb.AddForce(direction * throwForce, ForceMode.Impulse);
        damage = dm;
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
