using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObj_area : MonoBehaviour
{

    [SerializeField]
    int maxPlacedNum;

    int peopleCount;

    [SerializeField]
    private GameObject[] placedPrefabs;

    [SerializeField]
    private GameObject[] randomPos;

    [SerializeField]
    private Transform ref_pos;

    GameObject spawnedObject;
    List<GameObject> placedPrefabObjs = new List<GameObject>();
    List <int> list = new List<int> (); 

    void Start()
    {
        InvokeRepeating("SpawnEverySec", 12, 12);
        FillList();
    }
     
     void FillList()
     {
         for(int i = 0; i < randomPos.Length; i++)
         {
             list.Add(i);
         }
     }
     
     int GetNonRepeatRandom()
     {
         if(list.Count == 0){
             return -1; //  want to refill
         }
         int rand = Random.Range(0, list.Count);
         int value = list[rand];
         list.RemoveAt(rand);
         return value;
     }


    void SpawnEverySec()
    {
        /// Matrix People Spawn.
        if (peopleCount < maxPlacedNum)
        {
            int nonRepeatingNum = GetNonRepeatRandom();
            Vector3 mp_pos = randomPos[nonRepeatingNum].transform.position;
            spawnedObject = Instantiate(placedPrefabs[Random.Range(0, placedPrefabs.Length)], mp_pos, Quaternion.identity);
            peopleCount++;
            placedPrefabObjs.Add(spawnedObject);
        }
    }

    void Update()
    {
        Destroy(placedPrefabObjs[0], 12);  
    }
}
