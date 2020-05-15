using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileModelView : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Shoot(float projectileSpeed)
    {
        rb.AddRelativeForce(Vector3.forward * projectileSpeed * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            StartCoroutine(DelayedDestroy());
        }
    }

    private IEnumerator DelayedDestroy()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
