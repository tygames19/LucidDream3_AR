using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Call_Scene_Scheduler : MonoBehaviour
{

    private string systemTime;
    private int hour;
    private int min;
    private int sec;
    public string scheduledTime;
    public string calledSceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hour = System.DateTime.Now.Hour;
        min = System.DateTime.Now.Minute;
        sec = System.DateTime.Now.Second;
        systemTime = hour + ":" + min + ":" + sec;

        playScene(calledSceneName);
    }

    public void playScene(string level)
    {
        if (systemTime == scheduledTime)
        {
            SceneManager.LoadScene(level);
        }
    }
}
