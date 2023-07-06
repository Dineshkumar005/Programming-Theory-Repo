using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCrate : Crate
{
    public override void OnEnable()
    {
        hitPoints = 40;
    }

    public override void OnDisable()
    {
        if (isOld)
        {
            GameObject obj = ObjectPooler.SharedInstance.GetPooledObject(spawnPrefab.name);
            obj.transform.position = transform.position;
            obj.SetActive(true);
        }
        else
            isOld = true;
    }
}
