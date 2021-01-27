using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObj_area : MonoBehaviour
{

    [SerializeField]
    int maxPlacedNum;

    int placedNum;

    [SerializeField]
    private GameObject[] placedPrefabs;

    [SerializeField]
    private GameObject[] randomPos;

    [SerializeField]
    private Transform ref_pos;

    GameObject spawnedObject;
    List<GameObject> placedPrefabObjs = new List<GameObject>();

    //// Shuffle random number without repeating
    //int[] arrayNum = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 }; // = randomPos number
    //List<int> forShuffle = null;

    //int[] setNum = new int[] { 0, 1, 2, 3, 4, 5 }; // = constellation number
    //List<int> shuffleList = null;


    void Start()
    {
        InvokeRepeating("SpawnEverySec", 12, 12);
        //forShuffle.AddRange(arrayNum);
        //shuffleList.AddRange(setNum);
    }

    void SpawnEverySec()
    {
        /// Matrix People Spawn.
        if (placedNum < maxPlacedNum)
        {
            Vector3 mp_pos = randomPos[Random.Range(0, randomPos.Length)].transform.position;
            spawnedObject = Instantiate(placedPrefabs[Random.Range(0, placedPrefabs.Length)], mp_pos, ref_pos.rotation);
            placedPrefabObjs.Add(spawnedObject);
            placedNum++;
        }
    }

    void Update()
    {
            Destroy(placedPrefabObjs[0], 13);
    }


    //int GetUniqueRandom(bool reloadEmptyList)
    //{
    //    if (forShuffle.Count == 0)
    //    {
    //        if (reloadEmptyList)
    //        {
    //            forShuffle.AddRange(arrayNum);
    //        }
    //        else
    //        {
    //            return -1; // finite loop. 
    //        }
    //    }
    //    int rand = Random.Range(0, forShuffle.Count);
    //    int value = forShuffle[rand];
    //    forShuffle.RemoveAt(rand);
    //    return value;
    //}

    //int GetUniqueRandom2(bool reloadEmptyList2)
    //{
    //    if (shuffleList.Count == 0)
    //    {
    //        if (reloadEmptyList2)
    //        {
    //            shuffleList.AddRange(setNum);
    //        }
    //        else
    //        {
    //            return -1; // finite loop. 
    //        }
    //    }
    //    int rand = Random.Range(0, shuffleList.Count);
    //    int value = shuffleList[rand];
    //    shuffleList.RemoveAt(rand);
    //    return value;
    //}
}
