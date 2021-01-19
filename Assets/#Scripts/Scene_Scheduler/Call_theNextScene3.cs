using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class Call_theNextScene3: MonoBehaviour
{
    DateTime now;
    DateTime nextScene_scheduledTime;

    [SerializeField]
    private int year;
    [SerializeField]
    private int month;
    [SerializeField]
    private int day;

    [SerializeField]
    private int nextS_Hour;
    [SerializeField]
    private int nextS_Min;
    [SerializeField]
    private int nextS_Sec;

    void Start()
    {
        nextScene_scheduledTime = new DateTime(year, month, day, nextS_Hour, nextS_Min, nextS_Sec);
    }

  
    void Update()
    {
        now = DateTime.Now;
        int checkTime = DateTime.Compare(now, nextScene_scheduledTime);
        if (checkTime > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }
    }

}
