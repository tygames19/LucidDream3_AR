using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextBtn_down : MonoBehaviour
{
    public GameObject guide01;
    public GameObject guide02;
    public GameObject guide03;

    void Start()
    {
        guide01.SetActive(true);    
    }

    public void NextBtn_down01()
    {
        guide01.SetActive(false);
        guide02.SetActive(true);
    }

    public void NextBtn_down02()
    {
        guide02.SetActive(false);
        guide03.SetActive(true);
    }

    public void backToPreviousPage()
    {
        guide03.SetActive(false);
        guide02.SetActive(true);
    }
}
