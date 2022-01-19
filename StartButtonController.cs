using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonController : MonoBehaviour
{
    public GameObject character;
    public GameObject introUI;
    public GameObject inGameUI;
    private DemoMovementController demoMovementController;
    private FloorController floorController;

    private void Start() {
        character = GameObject.FindGameObjectWithTag("Player");
        floorController = GameObject.FindGameObjectWithTag("Floor").GetComponent<FloorController>();
        floorController.enable();
        demoMovementController = character.GetComponent<DemoMovementController>();
        demoMovementController.enabled = false;
        transform.parent.gameObject.GetComponent<GameController>().enabled = false;
        introUI.SetActive(true);
        inGameUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Gift" || other.gameObject.tag == "Snowball") {
            GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
            StartCoroutine(DisableUI());
        }
    }

    IEnumerator DisableUI() {
        yield return new WaitForSeconds(0.5f);
        floorController.moveAll(5f);
        gameObject.SetActive(false);
        introUI.SetActive(false);
        inGameUI.SetActive(true);
        //yield return new WaitForSeconds(1f);
        demoMovementController.enabled = true;
        transform.parent.gameObject.GetComponent<GameController>().enabled = true;
    }
}
