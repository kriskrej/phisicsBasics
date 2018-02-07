using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnMovement : MonoBehaviour {
    public float tresholdDistance = 0.3f;
    public float explosionRadius = 5f;
    public float explosionForce = 10f;

    Vector3 startPosition;
    void Start() {
        startPosition = transform.position;
    }

    void Update() {
        if (HasMoved())
            Explode();
    }

    bool HasMoved() {
        return Vector3.Distance(transform.position, startPosition) > tresholdDistance;
    }

    void Explode() {
        enabled = false;
        ThrowRigidbodiesAway();
        InstantiateParticles();
    }

    void InstantiateParticles() {
        Instantiate(Resources.Load("Ef_ExplosionParticle_02"), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void ThrowRigidbodiesAway() {
        var hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders) {
            var rb = hitCollider.GetComponent<Rigidbody>();
            if(rb == null) continue;
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }
    }
}

