using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Random = UnityEngine.Random;

public class SpreadObjs_onTheFloor : MonoBehaviour
{
    #region Properties
    //[SerializeField]
    //private GameObject[] randomObjArray;

    //[SerializeField]
    //private int maxNum = 0;

    //[SerializeField]
    //private Vector3 size;

    //int placedNum;

    bool placementPoseIsValid = false;

    ARRaycastManager arRaycastManager;
    List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    Pose placementPose;

    [SerializeField]
    private GameObject placementIndicator;

    [SerializeField]
    private Transform benchMark;

    public static event Action onSpawnObjValid;

    #endregion

    void Start()
    {
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        placementIndicator.SetActive(false);   
    }

    void Update()
    {
        UpdatePlacementPose();

        if (placementPoseIsValid == true && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //while (placedNum < maxNum)
            //{
            //    Vector3 spawnPos = placementPose.position + new Vector3(Random.Range(-size.x, size.x), 0, Random.Range(-size.z, size.z));
            //    Instantiate(randomObjArray[Random.Range(0, randomObjArray.Length)], spawnPos, placementPose.rotation);
            //    placedNum++;
            //}

            if (onSpawnObjValid != null)
            {
                onSpawnObjValid();
                benchMark.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
            }
        }

    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        arRaycastManager.Raycast(screenCenter, s_Hits, TrackableType.PlaneWithinPolygon);

        if (s_Hits.Count > 0)
        {
            placementPose = s_Hits[0].pose;

            /// rotate the placement indicator based on the camera direction.
            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);

            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);

            placementPoseIsValid = true;
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }
}
