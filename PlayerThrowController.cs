using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PlayerThrowController : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject snowballPrefab;
    public GameObject[] giftPrefabs;
    public AudioSource throwAudioSource;

    private bool leftOccupied, rightOccupied;
    private GameObject leftInHand, rightInHand;
    private Transform leftHandTrans, rightHandTrans;
    private float coolDownSnowballTimer, coolDownGiftTimer;

    private void Start() {
        coolDownGiftTimer = 1f;
        coolDownSnowballTimer = 1f;
    }

    void Update()
    {
        if (leftHandTrans == null)
        {
            leftHandTrans = leftHand.transform.Find("LeftRenderModel Slim(Clone)/vr_glove_left_model_slim(Clone)");
        }
        if (rightHandTrans == null)
        {
            rightHandTrans = rightHand.transform.Find("RightRenderModel Slim(Clone)/vr_glove_right_model_slim(Clone)");
        }

        if (coolDownSnowballTimer > 0f) {
            coolDownSnowballTimer -= Time.deltaTime;
            //Debug.Log("cool down snowball timer: " + coolDownSnowballTimer);
        }
        if (coolDownGiftTimer > 0f) {
            coolDownGiftTimer -= Time.deltaTime;
            //Debug.Log("cool down gift timer: " + coolDownGiftTimer);
        }

        if (leftHand.GetComponent<Hand>().IsGrabbingWithType(GrabTypes.Pinch)) {
            if (!leftOccupied && coolDownSnowballTimer < 0f && !GameController.isEnd) {
                leftInHand = Instantiate(snowballPrefab, leftHandTrans.position, leftHandTrans.rotation);
                leftHand.GetComponent<Hand>().AttachObject(leftInHand, GrabTypes.Pinch);
                leftOccupied = true;
                coolDownSnowballTimer = 1f;
            }
        }

        if (leftHand.GetComponent<Hand>().IsGrabEnding(leftInHand) && leftOccupied)
        {
            leftOccupied = false;
            leftHand.GetComponent<Hand>().DetachObject(leftInHand);
            Destroy(leftInHand, 5);
            throwAudioSource.Play();
        }

        if (rightHand.GetComponent<Hand>().IsGrabbingWithType(GrabTypes.Pinch)) {
            if (!rightOccupied && coolDownGiftTimer < 0f && !GameController.isEnd) {
                rightInHand = Instantiate(giftPrefabs[Random.Range(0,4)], rightHandTrans.position, rightHandTrans.rotation);
                rightHand.GetComponent<Hand>().AttachObject(rightInHand, GrabTypes.Pinch);
                rightOccupied = true;
                coolDownGiftTimer = 1f;
            }
        }

        if (rightHand.GetComponent<Hand>().IsGrabEnding(rightInHand) && rightOccupied)
        {
            rightOccupied = false;
            rightHand.GetComponent<Hand>().DetachObject(rightInHand);
            GameController.DecreaseGiftLeft();
            Destroy(rightInHand, 5);
            throwAudioSource.Play();
        }
    }
}
