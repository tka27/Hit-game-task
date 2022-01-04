using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    const string ENEMY_TAG = "Enemy";
    [SerializeField] float explosionForce;
    [SerializeField] float explosionRadius;
    public static event Action<Rigidbody> onEnemyHitEvent;
    Rigidbody selfRB;
    void Start()
    {
        selfRB = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision other)
    {
        var rb = other.rigidbody;

        if (rb != null && rb.tag == ENEMY_TAG)
        {
            var contact = other.GetContact(0);
            SceneData.singleton.hitParticles.transform.position = contact.point;
            SceneData.singleton.hitParticles.Play();
            onEnemyHitEvent.Invoke(rb);
            StartCoroutine(ApplyExplosion(rb, contact.point));
        }
        StartCoroutine(HideProjectile());
    }
    IEnumerator HideProjectile()
    {
        yield return new WaitForFixedUpdate();
        selfRB.HideRB();
    }

    IEnumerator ApplyExplosion(Rigidbody rb, Vector3 contact)
    {
        yield return new WaitForFixedUpdate();
        rb.AddExplosionForce(explosionForce, contact, explosionRadius);
    }
}
