using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCubes : MonoBehaviour
{
    [SerializeField]
    private int maxFallingNum;

    [SerializeField]
    private GameObject cubePrefab;

    [SerializeField]
    private Transform benchMark;

    [SerializeField]
    private Vector3 size;

    [SerializeField]
    private float height;

    [SerializeField]
    private float minInterval;

    [SerializeField]
    private float maxInterval;

    void Start()
    {
        StartCoroutine(SpawnFallingCubes());
    }

    IEnumerator SpawnFallingCubes()
    {
        /// falling cube Spawn.
        while (true)
        {
            int randomInterval = (int)Random.Range(minInterval, maxInterval);
            Vector3 randomPos = benchMark.position + new Vector3(Random.Range(-size.x, size.x), height, Random.Range(-size.z, size.z));
            cubePrefab.transform.localScale = new Vector3 (2, 2, 2);
            Instantiate(cubePrefab, randomPos, benchMark.rotation);
            yield return new WaitForSeconds(randomInterval);
        }
    }

}
