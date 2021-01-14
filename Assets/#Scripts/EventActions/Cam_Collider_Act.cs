using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Collider_Act : MonoBehaviour
{
    public GameObject explosion;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //other.gameObject.SetActive(false);
        Instantiate(explosion, other.transform.position, Quaternion.identity);
    }
}
