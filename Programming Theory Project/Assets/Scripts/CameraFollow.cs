using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerEmpty;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = playerEmpty.transform.position;
        transform.eulerAngles = new Vector3(playerEmpty.eulerAngles.x, playerEmpty.eulerAngles.y, playerEmpty.eulerAngles.z);
    }
}
