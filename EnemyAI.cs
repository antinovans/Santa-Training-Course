using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    /*    private float movementSpeed = 10f;*/
    /*    private Rigidbody enemyRb;*/
    private CharacterController controller;
    private GameObject player;
    private float playerSpeed;
/*    private float reactionDis = 50f;*/
    private bool soundPlayed;
    private bool hasCatchUp;
    private float stopDistance = 30f;
    private float initialXPos;

    public float toPlayerSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
/*        enemyRb = GetComponent<Rigidbody>();*/
        player = GameObject.FindGameObjectWithTag("Player");
        controller = GetComponent<CharacterController>();
        hasCatchUp = false;
        initialXPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        playerSpeed = player.GetComponent<DemoMovementController>().forwardSpeed;
        if(transform.position.z - player.transform.position.z > 20f)
        {
            hasCatchUp = true;
        }
        CatchUp();
        //if game ends delete object
        if(GameController.isEnd)
        {
            Destroy(gameObject);
        }
        /*float distance = Vector3.Distance(player.transform.position, transform.position);
        Vector3 movingDirection;

        if(distance <= reactionDis)
        {
            movingDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(movingDirection * movementSpeed);
            if (!soundPlayed) {
                GetComponent<AudioSource>().Play();
                soundPlayed = true;
            }
            
        }
        else
        {
            movingDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(movingDirection * 0.5f);
        }*/

        /*        if(transform.position.z + 4f < player.transform.position.z)
                {
                    //Debug.Log("haha");
                    Destroy(gameObject.transform.parent.gameObject);
                }*/

    }
    private void CatchUp()
    {
        
        if (!hasCatchUp)
        {
            Vector3 direction = new Vector3(0, Mathf.Sin(-Mathf.PI / 6), Mathf.Cos(-Mathf.PI / 6)).normalized;
            transform.Translate(direction * Time.deltaTime * (playerSpeed + 5f), Space.World);
  
        }
        else
        {
            if (Mathf.Abs(Vector3.Distance(player.transform.position, transform.position)) < 5f)
            {
                Destroy(gameObject);
                GameObject[] snowBlockParticles = GameObject.FindGameObjectsWithTag("SnowBlock");
                foreach(GameObject go in snowBlockParticles) {
                    go.GetComponent<ParticleSystem>().Play();
                    if (go.GetComponent<AudioSource>())
                        go.GetComponent<AudioSource>().Play();
                }
            }
            Vector3 forwardDirection = new Vector3(0, Mathf.Sin(-Mathf.PI / 6), Mathf.Cos(-Mathf.PI / 6)).normalized;
            Vector3 playerDirection = (player.transform.position - transform.position).normalized;
            Vector3 velocity = forwardDirection * playerSpeed + playerDirection * toPlayerSpeed;
            transform.Translate(velocity * Time.deltaTime, Space.World);
            if (!soundPlayed) {
                GetComponent<AudioSource>().Play();
                soundPlayed = true;
            }
            /*            transform.Translate(playerDirection * Time.deltaTime *15f, Space.World);*/
        }
    }
/*    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
        }

    }*/
}
