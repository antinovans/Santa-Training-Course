using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObstacleSpawner : MonoBehaviour
{
    //every 6f a obstacle may have chance to spawn
    public float spawnInterval = 10f;
    private float lastSpawnY = -30f;
    private float lastSpawnZ = 22f;
    private int spawnAmount = 10;
    public int obstacleSpawnChance;

    public List<GameObject> obstacles;
    public List<GameObject> existingObstacles;
    public GameObject mainCharacter;
    // Start is called before the first frame update
    void Start()
    {
        obstacleSpawnChance = 8;
        mainCharacter = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < spawnAmount; i++)
        {
            lastSpawnY += spawnInterval * Mathf.Sin(-Mathf.PI / 6);
            lastSpawnZ += spawnInterval * Mathf.Cos(Mathf.PI / 6);
            if (Random.Range(0, obstacleSpawnChance) == 0)
            {

                GameObject obstacle = obstacles[Random.Range(0, obstacles.Count)];
                GameObject newObstacle = (GameObject)Instantiate(obstacle, new Vector3(Random.Range(-20, 20), lastSpawnY, lastSpawnZ), Quaternion.Euler(0, 0, 0));
                existingObstacles.Add(newObstacle);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SpawnObstacles()
    {
        for (int i = 0; i < 3; i++)
        {
            lastSpawnY += spawnInterval * Mathf.Sin(-Mathf.PI / 6);
            lastSpawnZ += spawnInterval * Mathf.Cos(Mathf.PI / 6);
            if (Random.Range(0, obstacleSpawnChance) == 0)
            {

                GameObject obstacle = obstacles[Random.Range(0, obstacles.Count)];
                GameObject newObstacle = (GameObject)Instantiate(obstacle, new Vector3(Random.Range(-10, 10), lastSpawnY, lastSpawnZ), Quaternion.Euler(0, 0, 0));
                existingObstacles.Add(newObstacle);
            }
        }
    }
    //used to clear obstacles that are not in the camera sight
    public void deleteObstacles()
    {
        int numberToRemove = 0;
        for(int i = 0; i < existingObstacles.Count; i++)
        {
            if(existingObstacles[i].transform.position.z + 4f < mainCharacter.transform.position.z)
            {
                Destroy(existingObstacles[i]);
                numberToRemove++;
            }
        }
        //rearrange the list
        if (numberToRemove > 0)
        {
            existingObstacles.RemoveRange(0, Mathf.Min(numberToRemove, existingObstacles.Count));
        }
    }

    public void deleteAllObstacles()
    {
        for(int i = 0; i < existingObstacles.Count; i++)
        {
            Destroy(existingObstacles[i]);
        }
        existingObstacles.Clear();
    }

}
