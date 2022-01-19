using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public GameObject character;
    private DemoMovementController demoMovementController;
    public float stunDuration;
    public float speedUpDuration;
    // Start is called before the first frame update
    void Start()
    {
        stunDuration = 1f;
        speedUpDuration = 5f;
        character = GameObject.FindGameObjectWithTag("Player");
        demoMovementController = character.GetComponent<DemoMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Obstacles")
        {
            GetComponent<AudioSource>().Play();
            other.gameObject.SetActive(false);
            demoMovementController.isStunnedY = true;
            demoMovementController.isStunnedX = true;
            demoMovementController.forwardSpeed = 0f;
            demoMovementController.horizontalSpeed = 0f;
            StartCoroutine(SpeedUp());
            StartCoroutine(ResetStunFlag());
        }
    }

    IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(stunDuration);
        demoMovementController.isStunnedX = false;
        float time = 0;
        //Debug.Log("speed up");
        while(time < speedUpDuration)
        {
            demoMovementController.forwardSpeed = Mathf.Lerp(0f, 10f, time / speedUpDuration);
            demoMovementController.horizontalSpeed = Mathf.Lerp(0f, 10f, time / speedUpDuration);
            time += Time.deltaTime;
        }
    }
    IEnumerator ResetStunFlag()
    {
        yield return new WaitForSeconds(stunDuration + speedUpDuration);
        demoMovementController.isStunnedY = false;
        //Debug.Log("reset stun flag");
    }


}
