using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnimObj : MonoBehaviour
{

    [SerializeField]
    float delayTime;

    void Start()
    {
        Destroy(this.gameObject, delayTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
