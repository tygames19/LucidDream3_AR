using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{

    float currentTime =  0f;
    float startingTime =  10f;

    [SerializeField]
    private Text countNum;

    void Start()
    {
        currentTime = startingTime;
    }

   
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countNum.text = currentTime.ToString("0");

        if(currentTime <= 0)
        {
            currentTime = 0;
        }

        if (currentTime < 3)
        {
            countNum.color = Color.red;
        }
        else
        {
            countNum.color = Color.white;
        }
    }
}
