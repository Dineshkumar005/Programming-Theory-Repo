using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed;
    public int damage;
    
    void Update()
    {
        transform.Translate(transform.forward * speed);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.collider.CompareTag("Crate"))
        {
            other.gameObject.GetComponent<Crate>().DamageTaken(damage);
        }
    }
}
