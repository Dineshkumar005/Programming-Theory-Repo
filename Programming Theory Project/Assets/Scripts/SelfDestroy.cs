using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float lifeTime = 5f;
    public bool isAutoDestroyEnabled = false;
    private void OnEnable() {
        if(isAutoDestroyEnabled)
            Invoke("Disable",lifeTime);
    }

    void EndLife(float lifeSpan)
    {
        Invoke("Disable",lifeSpan);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        CancelInvoke("Disable");
    }
}
