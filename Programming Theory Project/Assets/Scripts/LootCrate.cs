using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inheritance
public class LootCrate : Crate
{
    public override void OnEnable()
    {
        hitPoints = 25;
    }

    //Polymorphism
    public override void OnDisable()
    {
        if(isOld){
            GameObject obj = ObjectPooler.SharedInstance.GetPooledObject(spawnPrefab.name);
            obj.transform.position = transform.position;
            obj.SetActive(true);
        }
        else
            isOld = true;
    }
}
