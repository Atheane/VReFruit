using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonProjectile : MonoBehaviour
{

    public float fireSpeed=400;
    public ParticleSystem fireParticle;
    void Start()
    {
        StartCoroutine(FireProjectileCoroutine());
    }

    IEnumerator FireProjectileCoroutine()
    {
        yield return new WaitForSeconds(4.0f);

        FireProjectile();

        yield return null;
    }

    public void FireProjectile()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        fireParticle.Play();
        GetComponent<Rigidbody>().AddForce(transform.forward * fireSpeed);
    }

}
