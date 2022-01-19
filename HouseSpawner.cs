using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseSpawner : MonoBehaviour
{
    private int initAmount = 6;
    //terrain chunk length
    private float houseSize = 30f;
    private float xPosRight = 25f;
    private float xPosLeft = -25f;
    private float lastYPos = 0f;
    private float lastZPos = -30f;

    public List<GameObject> typeOfHouses;
    public List<GameObject> existingHouses;

    public GameObject mainCharacter;
    // Start is called before the first frame update
    void Start()
    {
        mainCharacter = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < initAmount - 1; i++)
        {
            SpawnHouse();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnHouse()
    {
        GameObject houseLeft = typeOfHouses[Random.Range(0, typeOfHouses.Count)];
        GameObject houseRight = typeOfHouses[Random.Range(0, typeOfHouses.Count)];

        float yPos = lastYPos + houseSize * Mathf.Sin(-Mathf.PI / 6); 
        float zPos = lastZPos + houseSize * Mathf.Cos(Mathf.PI / 6); 

        GameObject newHouseLeft = (GameObject)Instantiate(houseLeft, new Vector3(xPosLeft, yPos, zPos), Quaternion.Euler(0,180,0));
        existingHouses.Add(newHouseLeft);
        GameObject newHouseRight = (GameObject)Instantiate(houseRight, new Vector3(xPosRight, yPos, zPos), Quaternion.Euler(0,0,0));
        existingHouses.Add(newHouseRight);



        lastYPos += houseSize * Mathf.Sin(-Mathf.PI / 6);
        lastZPos += houseSize * Mathf.Cos(Mathf.PI / 6);
    }

    public void deleteHouse()
    {


        int numberToRemove = 0;
        for (int i = 0; i < existingHouses.Count; i++) {
            if (existingHouses[i].transform.position.z + 12f < mainCharacter.transform.position.z) {
                Destroy(existingHouses[i]);
                numberToRemove++;
            }
        }
        //rearrange the list
        if (numberToRemove > 0) {
            existingHouses.RemoveRange(0, Mathf.Min(numberToRemove, existingHouses.Count));
        }
    }
}
