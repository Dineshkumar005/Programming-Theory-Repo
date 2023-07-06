using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float speed;
    public int damage;
    public float lifeSpan;
    private Rigidbody rb;
    public float defaultDistance=250f;

    private void Start() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit,defaultDistance))
            transform.LookAt(hit.point);
        else
            transform.LookAt(ray.origin+ray.direction*defaultDistance);

        rb=GetComponent<Rigidbody>();

        Destroy(gameObject, lifeSpan);
        rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.collider.CompareTag("Crate"))
        {
            other.gameObject.GetComponent<Crate>().DamageTaken(damage);
        }
        Destroy(gameObject);
    }
}
