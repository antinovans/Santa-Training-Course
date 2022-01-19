using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float power = 10f;
    public float duration = 1f;
    public Transform cameraTransform;
    public float slowDownAmount = 1f;
    public bool shouldShake;

    Vector3 startPos;
    float initialDuration;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("CameraKit").transform;
        initialDuration = duration;
        shouldShake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldShake)
        {
            cameraTransform.localPosition = startPos + Random.insideUnitSphere * power;
            duration -= Time.deltaTime * slowDownAmount;
        }

        if(duration < 0)
        {
            shouldShake = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacles")
        {
            duration = initialDuration;
            startPos = cameraTransform.localPosition;
            shouldShake = true;
        }
    }

}
