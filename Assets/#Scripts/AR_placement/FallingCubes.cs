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
    private GameObject trig_floor;

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
        trig_floor.SetActive(true);
        trig_floor.transform.SetPositionAndRotation(benchMark.position + new Vector3 (0, -0.6f, 0), benchMark.rotation);
        StartCoroutine(SpawnFallingCubes());
    }

    IEnumerator SpawnFallingCubes()
    {
        /// falling cube Spawn.
        while (true)
        {
            int randomInterval = (int)Random.Range(minInterval, maxInterval);
            Vector3 randomPos = benchMark.position + new Vector3(Random.Range(-size.x, size.x), height, Random.Range(0, size.z));
            Instantiate(cubePrefab, randomPos, benchMark.rotation);
            yield return new WaitForSeconds(randomInterval);
        }
    }

}
