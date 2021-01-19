using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class Call_theNextScene2 : MonoBehaviour
{
    [Header("Falling Cubes")]
    [SerializeField]
    private GameObject somethingToShowOnTime01;

    [Header("Destroy Matrix Pillar & People")]
    [SerializeField]
    private GameObject somethingToHideOnTime;

    DateTime now;
    DateTime nextAppear01_scheduledTime;
    DateTime nextScene_scheduledTime;

    [Header("Perform Date")]
    [SerializeField]
    private int year;
    [SerializeField]
    private int month;
    [SerializeField]
    private int day;

    [SerializeField]
    private Transform benchMark;

    [Header("St To Show On Time 01")]
    [SerializeField]
    private int nextA1_Hour;
    [SerializeField]
    private int nextA1_Min;
    [SerializeField]
    private int nextA1_Sec;

    [Header("Call the Next Scene")]
    [SerializeField]
    private int nextS_Hour;
    [SerializeField]
    private int nextS_Min;
    [SerializeField]
    private int nextS_Sec;

    [Header("Need To Turn Off")]
    [SerializeField]
    ARPlaneManager _arPlaneManager;

    [SerializeField]
    ARPointCloudManager _pointCloudManager;

    [SerializeField]
    GameObject matrixManager;

    [SerializeField]
    private GameObject guide01;

    [Header("Need To Turn On")]
    [SerializeField]
    private GameObject guide02;

    void Start()
    {
        nextAppear01_scheduledTime = new DateTime(year, month, day, nextA1_Hour, nextA1_Min, nextA1_Sec);
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

        // Something to show on time.
        int onTime01 = DateTime.Compare(now, nextAppear01_scheduledTime);
        if (onTime01 > 0)
        {
            _arPlaneManager.SetTrackablesActive(false);
            _arPlaneManager.enabled = false;
            _pointCloudManager.SetTrackablesActive(false);
            _pointCloudManager.enabled = false;

            matrixManager.SetActive(false);

            Destroy(somethingToHideOnTime);

            guide01.SetActive(false);
            guide02.SetActive(true);

            somethingToShowOnTime01.SetActive(true);
            somethingToShowOnTime01.transform.SetPositionAndRotation(benchMark.position, benchMark.rotation);
        }

    }

}
