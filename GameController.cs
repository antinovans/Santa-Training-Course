using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float timer;
    //static variable to tell other script that time's up
    public static bool isEnd;
    public SpawnManager spawnManager;
    public AudioSource backgroundAudioSource;
    public AudioClip endingClip;
    public static int giftLeftCount;
    public static int giftReceivedCount;
    public static Text giftReceivedText;
    public static Text giftLeftText;
    public GameObject inGameUI;

    private static bool gameStart;
    private float uiAppearInterval;
    private float timeLeft;

    void Start() {
        gameStart = true;
        isEnd = false;
        giftLeftCount = 60;
        uiAppearInterval =2f;
        timeLeft = timer;
        giftReceivedText = GameObject.FindGameObjectWithTag("GiftSent").GetComponent<Text>();
        giftLeftText = GameObject.FindGameObjectWithTag("GiftLeft").GetComponent<Text>();
        giftLeftText.text = giftLeftCount.ToString();
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown() {
        // Debug.Log("count down started");
        yield return new WaitForSeconds(timeLeft/2);
        timeLeft -= timeLeft/2;
        GameObject halfTimeUI = GameObject.Find("InGameUI/Canvas/HalfTimeUI");
        halfTimeUI.SetActive(true);
        yield return new WaitForSeconds(uiAppearInterval);
        timeLeft -= uiAppearInterval;
        halfTimeUI.SetActive(false);
        yield return new WaitForSeconds(timeLeft - timer/6);
        timeLeft -= timer/6;
        GameObject noTimeUI = GameObject.Find("InGameUI/Canvas/NoTimeUI");
        noTimeUI.SetActive(true);
        yield return new WaitForSeconds(uiAppearInterval);
        timeLeft -= uiAppearInterval;
        noTimeUI.SetActive(false);
        yield return new WaitForSeconds(timeLeft);

        isEnd = true;
        inGameUI.SetActive(false);
        backgroundAudioSource.clip = endingClip;
        backgroundAudioSource.Play();
        if (spawnManager != null) {
            spawnManager.EndGame();
        }
    }

    private void Update() {
        if (giftLeftCount == 0 && !isEnd) {
            isEnd = true;
            inGameUI.SetActive(false);
            backgroundAudioSource.clip = endingClip;
            backgroundAudioSource.Play();
            if (spawnManager != null) {
                spawnManager.EndGame();
            }
        }    
    }

    public static void AddGiftCount() {
        giftReceivedText.text = giftReceivedCount++.ToString();
    }

    public static void DecreaseGiftLeft() {
        if (gameStart) {
            giftLeftText.text = giftLeftCount--.ToString();
        }
           
    }
    
    public void RestartScene() {
        SceneManager.LoadScene("Final");
    }
}
