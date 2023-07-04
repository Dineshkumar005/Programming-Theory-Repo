using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCrate : Crate
{
    private void Start()
    {
        hitPoints = 40;
    }

    public override void OnDestroy()
    {
        Instantiate(spawnPrefab, transform.position, Quaternion.identity);
    }
}
