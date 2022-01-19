using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationSpawner : MonoBehaviour
{
    public float spawnInterval = 15f;
    private float lastSpawnY = -30f;
    private float lastSpawnZ = 22f;
    private int spawnAmount = 7;

    public List<GameObject> decorations;
    public List<GameObject> existingDecorations;
    public GameObject mainCharacter;
    // Start is called before the first frame update
    void Start()
    {
        mainCharacter = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < spawnAmount; i++) {
            lastSpawnY += spawnInterval * Mathf.Sin(-Mathf.PI / 6);
            lastSpawnZ += spawnInterval * Mathf.Cos(Mathf.PI / 6);
            if (Random.Range(0, 2) == 0) {

                GameObject decoration = decorations[Random.Range(0, decorations.Count)];
                GameObject newDecoration = (GameObject)Instantiate(decoration, new Vector3(0, lastSpawnY, lastSpawnZ), Quaternion.Euler(0, 0, 0));
                existingDecorations.Add(newDecoration);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnDecorations() {
        for (int i = 0; i < 2; i++) {
            lastSpawnY += spawnInterval * Mathf.Sin(-Mathf.PI / 6);
            lastSpawnZ += spawnInterval * Mathf.Cos(Mathf.PI / 6);
            if (Random.Range(0, 2) == 0) {

                GameObject decoration = decorations[Random.Range(0, decorations.Count)];
                GameObject newDecoration = (GameObject)Instantiate(decoration, new Vector3(0, lastSpawnY, lastSpawnZ), Quaternion.Euler(0, 0, 0));
                existingDecorations.Add(newDecoration);
            }
        }
    }
    public void DeleteDecorations() {
        int numberToRemove = 0;
        for (int i = 0; i < existingDecorations.Count; i++) {
            if (existingDecorations[i].transform.position.z + 4f < mainCharacter.transform.position.z) {
                Destroy(existingDecorations[i]);
                numberToRemove++;
            }
        }
        //rearrange the list
        if (numberToRemove > 0) {
            existingDecorations.RemoveRange(0, Mathf.Min(numberToRemove, existingDecorations.Count));
        }
    }
}
