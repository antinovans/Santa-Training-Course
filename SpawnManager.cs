using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public RoadSpawner roadSpawner;
    public HouseSpawner houseSpawner;
    public ObstacleSpawner obstacleSpawner;
    public EnemySpawner enemySpawner;
    public DecorationSpawner decorationSpawner;

    public int spawnTimes;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimes = 0;
        roadSpawner = GetComponent<RoadSpawner>();
        houseSpawner = GetComponent<HouseSpawner>();
        obstacleSpawner = GetComponent<ObstacleSpawner>();
        enemySpawner = GetComponent<EnemySpawner>();
        decorationSpawner = GetComponent<DecorationSpawner>();
    }

    // Update is called once per frame
    public void SpawnTriggerEnter()
    {
        spawnTimes++;
        if (!GameController.isEnd)
        {
            roadSpawner.MoveRoad();
            houseSpawner.SpawnHouse();
            houseSpawner.deleteHouse();
            obstacleSpawner.SpawnObstacles();
            obstacleSpawner.deleteObstacles();
            enemySpawner.SpawnEnemys();
            decorationSpawner.SpawnDecorations();
            decorationSpawner.DeleteDecorations(); ;
        }
    }
    public void EndGame()
    {
        obstacleSpawner.deleteAllObstacles();
        roadSpawner.SpawnEndingPrefab();
    }
    public void LevelUp() {
        if(spawnTimes == 5) {
            obstacleSpawner.obstacleSpawnChance -= 3;
        }
        if(spawnTimes == 10) {
            enemySpawner.maxNumOfEnemies = 2;
        }
        if(spawnTimes == 15) {
            obstacleSpawner.obstacleSpawnChance -= 3;
        }
        if(spawnTimes == 20) {
            enemySpawner.maxNumOfEnemies = 3;
        }
    }
}
