using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCrate : Crate
{
    private void Start() {
        hitPoints = 15;
    }

    public override void OnDestroy()
    {

    }
}
