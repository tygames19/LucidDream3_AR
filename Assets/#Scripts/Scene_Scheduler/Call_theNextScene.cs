using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Call_theNextScene : MonoBehaviour
{
    [SerializeField]
    private GameObject somethingToShowOnTime;

    [SerializeField]
    private GameObject somethingToHideOnTime;

    [SerializeField]
    private Transform ref_pos;

    DateTime now;
    DateTime nextAppear_scheduledTime;
    DateTime nextScene_scheduledTime;

    [SerializeField]
    private int year;
    [SerializeField]
    private int month;
    [SerializeField]
    private int day;

    [SerializeField]
    private int nextA_Hour;
    [SerializeField]
    private int nextA_Min;
    [SerializeField]
    private int nextA_Sec;

    [SerializeField]
    private int nextS_Hour;
    [SerializeField]
    private int nextS_Min;
    [SerializeField]
    private int nextS_Sec;

    void Start()
    {
        nextAppear_scheduledTime = new DateTime(year, month, day, nextA_Hour, nextA_Min, nextA_Sec);
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
        int onTime = DateTime.Compare(now, nextAppear_scheduledTime);
        if (onTime > 0)
        {
            Destroy(somethingToHideOnTime);  
            somethingToShowOnTime.SetActive(true);
            somethingToShowOnTime.transform.SetPositionAndRotation(ref_pos.position, ref_pos.rotation);
        }
    }

}
