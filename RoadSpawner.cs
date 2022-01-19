using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> roads;
    public GameObject EndingPrefab;
    //terrain chunk length 30 * sin(30)
    private static float spawnOffsetY = 30f * Mathf.Sin(-Mathf.PI / 6);
    //terrain chunk length 30 * cos(30)
    private static float spawnOffsetZ = 30f * Mathf.Cos(Mathf.PI /6);
    // Start is called before the first frame update
    void Start()
    {
        if(roads != null && roads.Count > 0)
        {
            roads = roads.OrderBy(r => r.transform.position.z).ToList();
        }
    }

    public void MoveRoad()
    {
        GameObject movedRoad = roads[0];
        roads.Remove(movedRoad);
        float newY = roads[roads.Count - 1].transform.position.y + spawnOffsetY;
        float newZ = roads[roads.Count - 1].transform.position.z + spawnOffsetZ;
        movedRoad.transform.position = new Vector3(0, newY, newZ);
        roads.Add(movedRoad);
    }

    public void SpawnEndingPrefab()
    {
        float endingY = roads[roads.Count - 1].transform.position.y + 20 * Mathf.Sin(-Mathf.PI / 6);
        float endingZ = roads[roads.Count - 1].transform.position.z + 20 * Mathf.Cos(-Mathf.PI / 6);
        GameObject endingPrefab = (GameObject)Instantiate(EndingPrefab, new Vector3(0, endingY, endingZ), Quaternion.Euler(0, 0, 0));
    }
}
