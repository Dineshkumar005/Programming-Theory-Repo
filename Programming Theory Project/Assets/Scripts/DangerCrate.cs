using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerCrate : Crate
{
    public float radius;
    public float explosionForce;
    public override void OnEnable()
    {
        hitPoints = 10;
    }

    public override void OnDisable()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var item in colliders)
        {
            if (item.gameObject.CompareTag("Crate"))
            {
                item.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, radius);
                item.GetComponent<SelfDestroy>().StartCoroutine("EndLife", 1);
                item.gameObject.SetActive(false);
            }
        }
    }
}
