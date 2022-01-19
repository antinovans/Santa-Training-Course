using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanFeedback : MonoBehaviour
{
    public AudioClip snowman_hit_sfx;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Snowball") {
            //Debug.Log("snow ball received");
            StartCoroutine(SnowmanDestryoyAfter(other));
        }
/*        else if(other.gameObject.tag == "Obstacles") {
            StartCoroutine(AnotherSnowmanDestryoyAfter());
        }*/
    }

    IEnumerator SnowmanDestryoyAfter(Collider other) {
        other.gameObject.transform.Find("snowball").gameObject.SetActive(false);
        /* other.gameObject.transform.Find("explosionVFX").gameObject.SetActive(true);*/
        gameObject.transform.Find("explosionVFX").gameObject.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(snowman_hit_sfx);
        yield return new WaitForSeconds(0.5f);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

/*    IEnumerator AnotherSnowmanDestryoyAfter() {
        gameObject.transform.Find("explosionVFX").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }*/
}
