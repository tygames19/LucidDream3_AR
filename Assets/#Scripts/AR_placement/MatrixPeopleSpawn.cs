using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixPeopleSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnedPrefab;

    [SerializeField]
    private GameObject matrixPeople;

    [SerializeField]
    private Vector3 size;

    [SerializeField]
    private int maxNum = 0;

    [SerializeField]
    private Transform benchMark;

    int peopleCount = 0;

    GameObject spawnedObject;
    GameObject matrixObj;
    List<GameObject> matrixPeopleArray = new List<GameObject>();
    List<GameObject> placedPrefabObjs = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnMatrixPillar());
        InvokeRepeating("SpawnPeopleEverySec", 8, 8);
    }
    void Update()
    {
        if (peopleCount >= maxNum)
        {
            peopleCount = 0;
        }

        Destroy(matrixPeopleArray[0], 10);
    }

    IEnumerator SpawnMatrixPillar()
    {
        while (placedPrefabObjs.Count < maxNum)
        {
            Vector3 spawnPos = benchMark.position + new Vector3(Random.Range(-size.x, size.x), 0, Random.Range(0, size.z));
            spawnedObject = Instantiate(spawnedPrefab, spawnPos, benchMark.rotation);
            placedPrefabObjs.Add(spawnedObject);
            yield return null;
        }
    }

    void SpawnPeopleEverySec()
    {
        /// Matrix People Spawn.
        if (matrixPeopleArray.Count < maxNum)
        {
            Vector3 mp_pos = placedPrefabObjs[peopleCount].transform.position;
            Quaternion mp_rot = Quaternion.Euler(placedPrefabObjs[peopleCount].transform.rotation.x, Random.Range(0, 180), placedPrefabObjs[peopleCount].transform.rotation.z);
            matrixObj = Instantiate(matrixPeople, mp_pos, mp_rot);
            matrixPeopleArray.Add(matrixObj);
        }
    }
}
