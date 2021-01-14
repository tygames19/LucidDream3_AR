using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextBtn_down : MonoBehaviour
{
    public GameObject firstGuide;
    public GameObject secondGuide;

    void Start()
    {
        firstGuide.SetActive(true);    
    }

    public void NextBtn_down()
    {
        firstGuide.SetActive(false);
        secondGuide.SetActive(true);
    }
}
