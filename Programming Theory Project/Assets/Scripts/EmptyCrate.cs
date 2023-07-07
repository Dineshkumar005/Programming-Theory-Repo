using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inheritance
public class EmptyCrate : Crate
{
    public override void OnEnable() {
        hitPoints = 15;
    }

    //Polymorphism
    public override void OnDisable()
    {
    }
}
