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
    private Transform ref_pos;

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
        trig_floor.transform.SetPositionAndRotation(ref_pos.position + new Vector3(0, 0, 2.5f), ref_pos.rotation);
        StartCoroutine(SpawnFallingCubes());
    }

    IEnumerator SpawnFallingCubes()
    {
        /// falling cube Spawn.
        while (true)
        {
            int randomInterval = (int)Random.Range(minInterval, maxInterval);
            Vector3 randomPos = ref_pos.position + new Vector3(Random.Range(-size.x, size.x), height, Random.Range(0, size.z));
            Instantiate(cubePrefab, randomPos, ref_pos.rotation);
            yield return new WaitForSeconds(randomInterval);
        }
    }

}
