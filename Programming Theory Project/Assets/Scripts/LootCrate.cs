using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootCrate : Crate
{
    private void Start() {
        hitPoints = 25;
    }

    public override void OnDestroy() {
        if (!isQuitting)
        {
            Instantiate(spawnPrefab, transform.position, Quaternion.identity);
        }
    }
}
