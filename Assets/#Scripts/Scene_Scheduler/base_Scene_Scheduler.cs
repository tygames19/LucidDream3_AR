﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class base_Scene_Scheduler : MonoBehaviour
{
    [Header("Perform Date")]
    [SerializeField]
    private int year;
    [SerializeField]
    private int month;
    [SerializeField]
    private int day;

    [Header("CountDown")]
    [SerializeField]
    private int nextA1_Hour;
    [SerializeField]
    private int nextA1_Min;
    [SerializeField]
    private int nextA1_Sec;

    [Header("Prologue")]
    [SerializeField]
    private int p_hour;
    [SerializeField]
    private int p_min;
    [SerializeField]
    private int p_sec;
    
    [Header("Scene02")]
    [SerializeField]
    private int s2_hour;
    [SerializeField]
    private int s2_Min;
    [SerializeField]
    private int s2_Sec;
    
    [Header("Scene03")]
    [SerializeField]
    private int s3_Hour;
    [SerializeField]
    private int s3_Min;
    [SerializeField]
    private int s3_Sec;
    
    [Header("Epilogue")]
    [SerializeField]
    private int e_Hour;
    [SerializeField]
    private int e_Min;
    [SerializeField]
    private int e_Sec;

    [Header("Guide 03 - Hide")]
    [SerializeField]
    private GameObject somethingToHideOnTime01;

    [SerializeField]
    private GameObject somethingToHideOnTime02;

    [Header("Guide 04 - Show / CountDown")]
    [SerializeField]
    private GameObject somethingToShowOnTime;

    DateTime now;// = DateTime.Now
    DateTime nextAppear01_scheduledTime;
    DateTime prologue; // = new DateTime(2021, 01, 06, 16, 00, 00); // 공연 시작 시간
    DateTime s2_scheduledTime; //= new DateTime(2021, 01, 06, 16, 03, 00); // 0 >> 1
    DateTime s3_scheduledTime; //= new DateTime(2021, 01, 06, 16, 06, 00); // 1 >> 2
    DateTime e_scheduledTime; //= new DateTime(2021, 01, 06, 16, 09, 00); // 3 >> 4

    void Start()
    {
        nextAppear01_scheduledTime = new DateTime(year, month, day, nextA1_Hour, nextA1_Min, nextA1_Sec);
        prologue = new DateTime(year, month, day, p_hour, p_min, p_sec);
        s2_scheduledTime = new DateTime(year, month, day, s2_hour, s2_Min, s2_Sec);
        s3_scheduledTime = new DateTime(year, month, day, s3_Hour, s3_Min, s3_Sec);
        e_scheduledTime = new DateTime(year, month, day, e_Hour, e_Min, e_Sec);

        //Debug.Log(DateTime.Now);
    }

    void Update()
    {
        now = DateTime.Now;
        int checkTime = DateTime.Compare(now, prologue);
        if (checkTime > 0)
        {
            if (DateTime.Compare(now, s2_scheduledTime) > 0)
            {
                if (DateTime.Compare(now, s3_scheduledTime) > 0)
                {
                    if (DateTime.Compare(now, e_scheduledTime) > 0)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4, LoadSceneMode.Single);
                    }
                    else
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3, LoadSceneMode.Single);
                    }
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2, LoadSceneMode.Single);
                }
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
            }
        }

        // Something to show on time.
        int onTime01 = DateTime.Compare(now, nextAppear01_scheduledTime);
        if (onTime01 > 0)
        {
            somethingToHideOnTime01.SetActive(false);
            somethingToHideOnTime02.SetActive(false);
            somethingToShowOnTime.SetActive(true);
        }
    }
}
