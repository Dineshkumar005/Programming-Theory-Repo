using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public float magnetSpeed=50f;
    public bool isFollowing = false;
    GameManager gameManager;

    public IEnumerator FollowPlayer()
    {
        gameManager= GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        isFollowing = true;
        GameObject playerTransform = GameObject.Find("Player");
        PlayerController playerController = playerTransform.GetComponent<PlayerController>();
        while( playerController.currentPowerUpType!=PowerupType.None && !gameManager.isGameOver)
        {
            Vector3 lookDirection = (playerTransform.transform.position - transform.position).normalized;
            transform.position += lookDirection * magnetSpeed * Time.deltaTime;
            yield return null;
        }
        isFollowing = false;
    }

    private void OnDisable() {
        gameObject.GetComponent<CoinScript>().StopAllCoroutines();
        isFollowing = false;
    }
}
