using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class base_Scene_Scheduler : MonoBehaviour
{
    [SerializeField]
    private int year;
    [SerializeField]
    private int month;
    [SerializeField]
    private int day;
    [SerializeField]
    private int hour;
    [SerializeField]
    private int min;
    [SerializeField]
    private int sec;
    [SerializeField]
    private int s1_Min;
    [SerializeField]
    private int s1_Sec;
    [SerializeField]
    private int s2_Hour;
    [SerializeField]
    private int s2_Min;
    [SerializeField]
    private int s2_Sec;
    [SerializeField]
    private int s3_Hour;
    [SerializeField]
    private int s3_Min;
    [SerializeField]
    private int s3_Sec;

    DateTime now;// = DateTime.Now

    DateTime perfromTime; // = new DateTime(2021, 01, 06, 16, 00, 00); // 공연 시작 시간
    DateTime s1_scheduledTime; //= new DateTime(2021, 01, 06, 16, 03, 00); // 0 >> 1
    DateTime s2_scheduledTime; //= new DateTime(2021, 01, 06, 16, 06, 00); // 1 >> 2
    DateTime s3_scheduledTime; //= new DateTime(2021, 01, 06, 16, 09, 00); // 3 >> 4

    void Start()
    {
        perfromTime = new DateTime(year, month, day, hour, min, sec);
        s1_scheduledTime = new DateTime(year, month, day, hour, s1_Min, s1_Sec);
        s2_scheduledTime = new DateTime(year, month, day, s2_Hour, s2_Min, s2_Sec);
        s3_scheduledTime = new DateTime(year, month, day, s3_Hour, s3_Min, s3_Sec);

        //Debug.Log(DateTime.Now);
    }

    void Update()
    {
        now = DateTime.Now;
        int checkTime = DateTime.Compare(now, perfromTime);
        if (checkTime > 0)
        {
            if (DateTime.Compare(now, s1_scheduledTime) > 0)
            {
                if (DateTime.Compare(now, s2_scheduledTime) > 0)
                {
                    if (DateTime.Compare(now, s3_scheduledTime) > 0)
                    {
                        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4, LoadSceneMode.Single);
                    }
                    else
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
                    }
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
                }
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
