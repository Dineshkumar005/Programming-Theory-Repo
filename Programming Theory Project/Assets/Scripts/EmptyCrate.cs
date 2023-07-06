using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCrate : Crate
{
    public override void OnEnable() {
        hitPoints = 15;
    }

    public override void OnDisable()
    {
    }
}
