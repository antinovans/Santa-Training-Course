using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class DemoMovementController : MonoBehaviour
{
    public SpawnManager spawnManager;
    public float forwardSpeed;
    public bool isStunnedY;
    public bool isStunnedX;
    public float horizontalSpeed;
    public float zAccelerationPerSec;
    public float zAccelerationMax;
    public GameObject endUI;
    public AudioSource skiAudioSource;

    private bool istriggered;
    private FloorController floorController;
    private CharacterController controller;
    private Vector3 forwardDirection;
    private Vector3 horizontalDirection;
    private float zVelocityMax = 20f;
    private float zVelocityMin = 10f;
    private bool floorLRTilted;
    private bool floorForwardTilted;
    private float floorPreviousHorizontalStatus; //<0 is tilted left, >0 ir right

    //tracking
    Vector3 previousPosition;
    Vector3 lastMoveDirection;

    private PlayerControl playerMovement;

    private void Awake()
    {
        playerMovement = new PlayerControl();
        floorController = GameObject.FindGameObjectWithTag("Floor").GetComponent<FloorController>();
    }

    private void OnEnable()
    {
        playerMovement.Enable();
        skiAudioSource.enabled = true;
    }
    private void OnDisable()
    {
        playerMovement.Disable();
        skiAudioSource.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        //tracking
        previousPosition = transform.position;
        lastMoveDirection = Vector3.zero;

        controller = GetComponent<CharacterController>();
        horizontalDirection = new Vector3(0, 0, 0);
        horizontalSpeed = 10f;
        zAccelerationPerSec = 10f;
        zAccelerationMax = 10f;

        forwardSpeed = 10f;
        //moving direction z = -30 degree
        forwardDirection = new Vector3(0, Mathf.Sin(-Mathf.PI / 6), Mathf.Cos(Mathf.PI / 6)).normalized;
        istriggered = false;
        isStunnedY = false;
        isStunnedX = false;
    }
    //used to track the moving direction
    //private void FixedUpdate()
    //{
    //    if (transform.position != previousPosition)
    //    {
    //        lastMoveDirection = (transform.position - previousPosition).normalized;
    //        previousPosition = transform.position;
    //    }
    //}
    // Update is called once per frame
    void Update() {
        if (!GameController.isEnd)
        {
            //Horizontal control
            if (!isStunnedX)
            {
                skiAudioSource.enabled = true;
                if (transform.position.x > 14.89f)
                {
                    horizontalDirection.x = Mathf.Clamp(Input.GetAxis("Horizontal"),-1, 0);
                    StartCoroutine(FloorMovementLR(horizontalDirection.x));
                    /*horizontalDirection.x = Mathf.Clamp(playerMovement.AxisControl.MoveHorizontal.ReadValue<float>(), -1, 0);*/
                }
                else if (transform.position.x < -14.89f)
                {
                    horizontalDirection.x = Mathf.Clamp(Input.GetAxis("Horizontal"), 0, 1);
                    StartCoroutine(FloorMovementLR(horizontalDirection.x));
                    /*horizontalDirection.x = Mathf.Clamp(playerMovement.AxisControl.MoveHorizontal.ReadValue<float>(), 0, 1);*/
                }
                else
                {
                    horizontalDirection.x = Input.GetAxis("Horizontal");
                    StartCoroutine(FloorMovementLR(horizontalDirection.x));
                    /*horizontalDirection.x = playerMovement.AxisControl.MoveHorizontal.ReadValue<float>();*/
                }
            } else {
                skiAudioSource.enabled = false;
                StartCoroutine(FloorMovementStunned());
            }
            //Vertical control
            if (!isStunnedY)
            {
                if (forwardSpeed > zVelocityMax)
                {
                    forwardSpeed += zAccelerationPerSec * Mathf.Clamp(Input.GetAxis("Vertical"), -1f, 0f) * Time.deltaTime;
                    StartCoroutine(FloorMovementForward(Mathf.Clamp(Input.GetAxis("Vertical"), -1f, 0f)));
                    /*forwardSpeed += zAccelerationPerSec * Mathf.Clamp(playerMovement.AxisControl.MoveVertical.ReadValue<float>(), -1f, 0f) * Time.deltaTime;*/
                }
                else if (forwardSpeed < zVelocityMin)
                {
                    forwardSpeed += zAccelerationPerSec * Mathf.Clamp(Input.GetAxis("Vertical"), 0f, 1f) * Time.deltaTime;
                    StartCoroutine(FloorMovementForward(Mathf.Clamp(Input.GetAxis("Vertical"), 0f, 1f)));
                    /*forwardSpeed += zAccelerationPerSec * Mathf.Clamp(playerMovement.AxisControl.MoveVertical.ReadValue<float>(), 0f, 1f) * Time.deltaTime;*/
                }
                else
                {
                    forwardSpeed += zAccelerationPerSec * Input.GetAxis("Vertical") * Time.deltaTime;
                    StartCoroutine(FloorMovementForward(Input.GetAxis("Vertical")));
                    /* forwardSpeed += zAccelerationPerSec * playerMovement.AxisControl.MoveVertical.ReadValue<float>() * Time.deltaTime;*/
                }
            }
            controller.Move(forwardDirection * Time.deltaTime * forwardSpeed + horizontalDirection * Time.deltaTime * horizontalSpeed);
        }
        else
        {
            //game ends and the MC will automatically moves to the end point
            floorController.resetFloor();
            GameObject destination = GameObject.FindGameObjectWithTag("Destination");
            Vector3 direction = (destination.transform.position - transform.position).normalized;
            transform.Translate(20f * direction * Time.deltaTime, Space.World);
            //reaches the end point
            if(Mathf.Abs(Vector3.Distance(destination.transform.position, transform.position)) < 5)
            {
                endUI.SetActive(true);
                //delete movement control script
                Destroy(this);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SpawnTrigger" && !istriggered)
        {
            //Debug.Log("fuck");
            spawnManager.SpawnTriggerEnter();
            spawnManager.LevelUp();
            istriggered = true;
            StartCoroutine("Cooldown");
        }
    }
    //internal cooldown in case of OnTriggerEnter fan bing
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(.5f);
        istriggered = false;
    }

    IEnumerator FloorMovementLR(float horizontal) {
        //Debug.Log("floor movement called");
        if ((floorPreviousHorizontalStatus < 0 && horizontal >= 0) || (floorPreviousHorizontalStatus > 0 && horizontal <= 0)) {
            floorController.moveAll(5f);
            yield return new WaitForSeconds(1f);
            floorLRTilted = false;
            floorPreviousHorizontalStatus = 0;
        }
        if (!floorLRTilted && horizontal != 0) {
            if (horizontal < 0) {
                floorController.raiseRight(5f);
            }
            else if (horizontal > 0) {
                floorController.raiseLeft(5f);
            }
            floorPreviousHorizontalStatus = horizontal;
            floorLRTilted = true;
            yield return new WaitForSeconds(1f);
        } else {
            yield return null;
        }
    }

    IEnumerator FloorMovementForward(float forwardAcc) {
        if (!floorForwardTilted && forwardAcc != 0) {
            if (forwardAcc < 0) {
                floorController.raiseFront(5f);
            }
            else if (forwardAcc > 0) {
                floorController.raiseBack(5f);
            }
            floorForwardTilted = true;
            yield return new WaitForSeconds(2f);
            floorController.moveAll(5f);
            yield return new WaitForSeconds(1f);
            floorForwardTilted = false;
        } else {
            yield return null;
        }
    }

    IEnumerator FloorMovementStunned() {
        floorController.moveAll(10f);
        yield return new WaitForSeconds(0.5f);
        floorController.moveAll(0f);
        yield return new WaitForSeconds(0.5f);
        floorController.moveAll(5f);
    }
}
