using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public GameObject license;
    public GameObject tryAgain;

    void Start()
    {
        GetComponent<Text>().text = GameController.giftReceivedCount.ToString();

        if (GameController.giftReceivedCount < 10) {
            tryAgain.SetActive(true);
        } else {
            license.SetActive(true);
        }
    }

}
