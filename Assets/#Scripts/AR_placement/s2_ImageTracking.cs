using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class s2_ImageTracking : MonoBehaviour
{
    [SerializeField]
    ARTrackedImageManager arimageManager;

    void OnEnable()
    {
        arimageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        arimageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
    {
        //foreach (ARTrackedImage whatToShow in obj.added)
        //{
        //    if (whatToShow.referenceImage.name == "R2")
        //    {
                
        //    }
        //}

    }

}
