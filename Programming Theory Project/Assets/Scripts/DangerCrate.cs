using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerCrate : Crate
{
    public float radius;
    public float explosionForce;
    private void Start()
    {
        hitPoints = 10;
    }

    public override void OnDestroy()
    {
        if (!isQuitting)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (var item in colliders)
            {
                if (item.gameObject.CompareTag("Crate"))
                {
                    item.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, radius);
                    Destroy(item.gameObject, 1f);
                }
            }
        }
    }
}
