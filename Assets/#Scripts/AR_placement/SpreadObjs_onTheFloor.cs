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

    bool placementPoseIsValid = false;
    bool benchMarkIsSet = false;
    //List<GameObject> placedObjPrefabs = new List<GameObject>();
    //GameObject spawnedObj;

    ARRaycastManager arRaycastManager;
    List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    Pose placementPose;

    [SerializeField]
    private GameObject bodyParts;

    [SerializeField]
    private GameObject placementIndicator;

    public static event Action onSpawnObjValid;

    #endregion

    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        placementIndicator.SetActive(false);
        benchMarkIsSet = false;
    }

    void Update()
    {
        UpdatePlacementPose();

        if (placementPoseIsValid == true && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //while (placedObjPrefabs.Count < maxNum)
            //{
            //    Vector3 spawnPos = placementPose.position + new Vector3(Random.Range(-size.x, size.x), 0, Random.Range(0, size.z));
            //    spawnedObj = Instantiate(randomObjArray[Random.Range(0, randomObjArray.Length)], spawnPos, placementPose.rotation);
            //    placedObjPrefabs.Add(spawnedObj);
            //}

            if (!benchMarkIsSet)
            {
                Vector3 spawnPos = placementPose.position + new Vector3(0, -0.7f, 0);
                bodyParts.SetActive(true);
                bodyParts.transform.SetPositionAndRotation(spawnPos, placementPose.rotation);
                benchMarkIsSet = true;
            }

            if (onSpawnObjValid != null)
            {
                onSpawnObjValid();
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
