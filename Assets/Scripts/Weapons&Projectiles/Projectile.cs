using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float throwForce;
    [SerializeField] private float destroyDelay;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ThrowProjectile(Vector3 direction)
    {
        Debug.Log(rb);
        rb.AddForce(direction * throwForce, ForceMode.Impulse);
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
