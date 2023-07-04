using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Crate : MonoBehaviour
{
    public int hitPoints;
    public GameObject spawnPrefab;

    public void DamageTaken(int damage)
    {
        hitPoints -= damage;
        if(hitPoints<=0)
            Destroy(gameObject);
    }

    public abstract void OnDestroy();
}
