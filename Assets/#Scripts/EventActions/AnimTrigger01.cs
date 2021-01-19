using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class AnimTrigger01 : MonoBehaviour
{
    [SerializeField]
    private GameObject guide01;

    [SerializeField]
    private GameObject guide03;

    public void AnimTrigger()
    {
        guide01.SetActive(false);
        guide03.SetActive(true);

        var abc = GetComponent<ARTrackedImageManager>();
        abc.enabled = true;
    }
}
