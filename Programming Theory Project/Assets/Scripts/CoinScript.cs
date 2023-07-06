using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public float magnetSpeed=50f;
    public bool isFollowing = false;

    public IEnumerator FollowPlayer()
    {
        isFollowing = true;
        GameObject playerTransform = GameObject.Find("Player");
        while(PlayerController.currentPowerUpType!=PowerupType.None  && !GameManager.isGameOver)
        {
            Vector3 lookDirection = (playerTransform.transform.position - transform.position).normalized;
            transform.position += lookDirection * magnetSpeed * Time.deltaTime;
            yield return null;
        }
        isFollowing = false;
    }
}
