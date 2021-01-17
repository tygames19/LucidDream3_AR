using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Btn_toLoad_nextScene : MonoBehaviour
{
    //[SerializeField]
    //GameObject paperPlane;

    //[SerializeField]
    //GameObject constellation;

    //[SerializeField]
    //GameObject nextBtnToLoadScene;

    //[SerializeField]
    //GameObject nextBtnToShowObj;

    public void btnToloadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }

    //public void btnToShowObj()
    //{
    //    Destroy(paperPlane);
    //    constellation.SetActive(true);
    //    nextBtnToShowObj.SetActive(false);
    //    nextBtnToLoadScene.SetActive(true);
    //}
}
