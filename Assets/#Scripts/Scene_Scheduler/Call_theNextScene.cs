using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Call_theNextScene : MonoBehaviour
{
   // public GameObject somethingToShowOnTime;

    DateTime now;
    DateTime nextScene_scheduledTime;

    public int year;
    public int month;
    public int day;
    public int next_Hour;
    public int next_Min;
    public int next_Sec;

    void Start()
    {
        nextScene_scheduledTime = new DateTime(year, month, day, next_Hour, next_Min, next_Sec);
    }

  
    void Update()
    {
        now = DateTime.Now;
        int checkTime = DateTime.Compare(now, nextScene_scheduledTime);
        if (checkTime > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }

        /// Something to show on time.
        //int onTime = DateTime.Compare(now, s1_scheduledTime);
        //if (onTime > 0)
        //{
        //    somethingToShowOnTime.SetActive(true);
        //}
    }

}
