using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Crate : MonoBehaviour
{
    public int hitPoints;
    public GameObject spawnPrefab;
    public bool isOld = false;

    public void DamageTaken(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
            gameObject.SetActive(false);
    }

    public abstract void OnEnable();

    public abstract void OnDisable();
}
