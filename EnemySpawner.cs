using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class EnemySpawner : MonoBehaviour
{
    /*    public float spawnInterval = 30f;
        private float lastSpawnY = -30f;
        private float lastSpawnZ = 22f;*/
    //each chunk has 10 chances to spawn a enemy
    public List<GameObject> spawnPoints;
    private int spawnAmount = 2;
    private List<GameObject> potentialSpawnPoints;
    private GameObject mainCharacter;
    public float spawnOffset = 10f;
    public int enemySpawnChance;
    public int maxNumOfEnemies;

    public List<GameObject> enemies;
    private List<GameObject> existingEnemies;
    /*    public List<GameObject> existingEnemies;*/
    // Start is called before the first frame update
    void Start()
    {
        enemySpawnChance = 2;
        maxNumOfEnemies = 1;
        mainCharacter = GameObject.FindGameObjectWithTag("Player");
        potentialSpawnPoints = new List<GameObject>();
        existingEnemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        /*Debug.Log(existingEnemies.Count);*/
    }
    public void SpawnEnemys()
    {
        existingEnemies.RemoveAll(item => item == null);
        if (existingEnemies.Count < maxNumOfEnemies) {
            GetNewestArray();
            for (int i = 0; i < potentialSpawnPoints.Count; i++) {
                if (Random.Range(0, enemySpawnChance) == 0) {
                    if (existingEnemies.Count < maxNumOfEnemies) {
                        GameObject enemy = enemies[Random.Range(0, enemies.Count)];
                        GameObject newEnemy = (GameObject)Instantiate(enemy, new Vector3(potentialSpawnPoints[i].transform.position.x, mainCharacter.transform.position.y + Mathf.Sin(Mathf.PI / 6) * spawnOffset, mainCharacter.transform.position.z - Mathf.Cos(Mathf.PI / 6) * spawnOffset), Quaternion.Euler(30, 0, 0));
                        existingEnemies.Add(newEnemy);
                    }
                }
            }
        }

/*        for (int i = 0; i < spawnAmount; i++) {
            lastSpawnY += spawnInterval * Mathf.Sin(-Mathf.PI / 6);
            lastSpawnZ += spawnInterval * Mathf.Cos(Mathf.PI / 6);
            if (Random.Range(0, 3) == 0) {

                GameObject enemy = enemies[Random.Range(0, enemies.Count)];
                GameObject newEnemy = (GameObject)Instantiate(enemy, new Vector3(Random.Range(-10, 10), lastSpawnY, lastSpawnZ), Quaternion.Euler(0, 0, 0));
            }
        }*/
    }
    public void GetNewestArray()
    {
        if (spawnPoints.Count > 0)
        {
            potentialSpawnPoints.Clear();
            float minimum = 20;
            int minIndex = 0;
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                if(minimum > Mathf.Abs(spawnPoints[i].transform.position.x - mainCharacter.transform.position.x))
                {
                    minimum = Mathf.Abs(spawnPoints[i].transform.position.x - mainCharacter.transform.position.x);
/*                    Debug.Log(i);
                    Debug.Log(minimum);*/
                    minIndex = i;
                }
                //spawnPointsDis[i] = Mathf.Abs(spawnPoints[i].transform.position.x - mainCharacter.transform.position.x);
            }
            for(int i = 0; i < spawnPoints.Count; i++)
            {
                if(i != minIndex)
                {
                    potentialSpawnPoints.Add(spawnPoints[i]);
/*                    Debug.Log(spawnPoints[i].transform.position.x);*/
                }
            }
            //spawnPointsDis.OrderByDescending(c => c).ToArray();

        }
    }
}
