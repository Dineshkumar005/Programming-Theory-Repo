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
    public Vector3 fireOffset;
    public float rateOfFire;
    private float rofCounter;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(Cursor.lockState == CursorLockMode.None)
                Cursor.lockState = CursorLockMode.Locked;
            else
                Cursor.lockState = CursorLockMode.None;
        }

        if(Input.GetMouseButton(0) && Cursor.lockState==CursorLockMode.Locked)
            Fire();

        MyInput();
        SpeedControl();
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
        if (newPosition.x < range)
            newPosition.x = -range;
        if (newPosition.z > range)
            newPosition.z = range;
        if (newPosition.z < range)
            newPosition.z = -range;

        transform.position = newPosition;
    }

    void Fire()
    {
        rofCounter += Time.deltaTime;
        if (rofCounter > rateOfFire)
        {
            Instantiate(projectilePrefab, transform.position + fireOffset, playerEmpty.transform.rotation);
            rofCounter = 0;
        }
    }
}
