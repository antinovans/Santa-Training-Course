using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseFeedback : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Gift") {
            //Debug.Log("gift received");
            GameController.AddGiftCount();
            Destroy(other.gameObject);
            GetComponent<AudioSource>().Play();
            transform.Find("Gift").gameObject.SetActive(false);
            transform.Find("happyface").gameObject.SetActive(true);
        }
    }
}
