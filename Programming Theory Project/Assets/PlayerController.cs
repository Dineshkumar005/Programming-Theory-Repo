using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Controle")]
    public GameObject playerEmpty;
    public float sens;
    public float movementSpeed;
    public float range;
    private Rigidbody playerRb;
    private float verticalInput;
    private float horizontalInput;
    private float mouseX;
    private float mouseY;


    [Header("Attack")]
    public GameObject projectilePrefab;
    public Transform fireInitPos;
    public float rateOfFire;
    private float rofCounter;

    
    [Header("Special Attacks")]
    public float magnetDuration;
    public float magnetRadius;
    public static PowerupType currentPowerUpType = PowerupType.None;
    private Coroutine magnetCoroutine;


    private GameManager gameManager;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !GameManager.isGameOver && !GameManager.isGamePaused)
        {
            if(Cursor.lockState == CursorLockMode.None)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
        }

        if(Input.GetMouseButton(0) && Cursor.lockState==CursorLockMode.Locked)
            Fire();

        if(!GameManager.isGameOver)
        {
            MyInput();
            SpeedControl();
        }
        CheckBoundary();
    }

    void MyInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        Vector3 direction = transform.forward * verticalInput + transform.right * horizontalInput;
        playerRb.AddForce(direction.normalized * movementSpeed * 10f, ForceMode.Force);


        if(Cursor.lockState==CursorLockMode.Locked)
        {
            float mouPosX = Input.GetAxisRaw("Mouse X") * sens;
            float mouPosY=Input.GetAxisRaw("Mouse Y") * sens;

            mouseX += mouPosY;
            mouseY -= mouPosX;
            mouseX = Mathf.Clamp(mouseX, -60f, 60f);

            transform.rotation = Quaternion.Euler(0,mouseY,0);
            playerEmpty.transform.rotation = Quaternion.Euler(mouseX, mouseY, 0);
        }

    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(playerRb.velocity.x, 0f, playerRb.velocity.z);
        if(flatVel.magnitude>movementSpeed)
        {
            Vector3 limitVel = flatVel.normalized * movementSpeed;
            playerRb.velocity = new Vector3(limitVel.x, playerRb.velocity.y, limitVel.z);
        }
    }

    void CheckBoundary()
    {
        Vector3 newPosition = transform.position;
        if (newPosition.x > range)
            newPosition.x = range;
        if (newPosition.x < -range)
            newPosition.x = -range;
        if (newPosition.z > range)
            newPosition.z = range;
        if (newPosition.z < -range)
            newPosition.z = -range;

        transform.position = newPosition;
    }

    void Fire()
    {
        rofCounter += Time.deltaTime;
        if (rofCounter > rateOfFire)
        {
            Instantiate(projectilePrefab, fireInitPos.position,Quaternion.identity);
            rofCounter = 0;
        }
    }

    private void OnTriggerEnter(Collider other) {

        if(other.CompareTag("Coin"))
        {
            gameManager.AddScore(5);
        }
        else if(other.CompareTag("PowerUp"))
        {
            currentPowerUpType=other.GetComponent<PowerUp>().powerupType;

            switch(currentPowerUpType)
            {
                case PowerupType.Magnet:
                {
                    if(magnetCoroutine !=null)
                        StopCoroutine(magnetCoroutine);
                    magnetCoroutine= StartCoroutine(Magnet());
                    break;
                }
                default :
                {
                        break;
                    }
            }
        }

        Destroy(other.gameObject);
    }


    IEnumerator Magnet()
    {
        float counter=0;
        while(counter<=magnetDuration)
        {
            counter+=Time.deltaTime;
            Collider[] colliders=Physics.OverlapSphere(transform.position,magnetRadius);
            
            foreach(Collider collider in colliders)
            {
                if(collider.CompareTag("Coin"))
                {
                    CoinScript cs = collider.GetComponent<CoinScript>();
                    if(!cs.isFollowing)
                        cs.StartCoroutine("FollowPlayer");
                }
            }

            yield return null;
        }

        currentPowerUpType = PowerupType.None;
    }
}
