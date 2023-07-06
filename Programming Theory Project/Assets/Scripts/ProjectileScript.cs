using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float radius;
    public int damage;
    private Rigidbody rb;


    private void OnCollisionEnter(Collision other) {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag("Crate"))
            {
                collider.gameObject.GetComponent<Crate>().DamageTaken(damage);
            }
        }
        gameObject.SetActive(false);
    }


    void OnDisable() {
        rb=GetComponent<Rigidbody>();
        rb.velocity=Vector3.zero;
    }
}
