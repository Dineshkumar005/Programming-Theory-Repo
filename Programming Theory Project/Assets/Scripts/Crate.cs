using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Crate : MonoBehaviour
{
    public int hitPoints;
    public GameObject spawnPrefab;
    public bool isQuitting = false;

    public void DamageTaken(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
            Destroy(gameObject);
    }

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    public abstract void OnDestroy();
}
