using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_TS0_CallNextPart : MonoBehaviour
{
    public GameObject guide02;
    public GameObject guide01;

    void Start()
    {
        guide01.SetActive(true);
    }

    
    void Update()
    {
        
    }

    public void callNext()
    {
        StartCoroutine(LateCall());
    }

    IEnumerator LateCall()
    {
        guide01.SetActive(false);
        yield return new WaitForSeconds(1);
        guide02.SetActive(true);
    }
}
