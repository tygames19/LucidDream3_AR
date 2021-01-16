using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Random = UnityEngine.Random;

public class TapToPlaceOnPlane : MonoBehaviour
{
    #region Properties

    [SerializeField]
    private GameObject[] randomObjectsArray;

    [SerializeField]
    private int maxNumForSpawn = 1;
    int palcedObjNum = 0;

    List<GameObject> placedPrefabObjs = new List<GameObject>();
    bool placementPoseIsValid = false;

    GameObject spawnedObject;

    ARRaycastManager arRaycastManager;
    List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    Pose placementPose;

    [SerializeField]
    private GameObject placementIndicator;

    [SerializeField]
    private Transform benchMark;

    public static event Action onPlacedObjValid;

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
            if (palcedObjNum < maxNumForSpawn)
            {
                PlaceObjects();

            }
            else
            {
                Destroy(placedPrefabObjs[0].gameObject);
                placedPrefabObjs.RemoveAt(0);
                palcedObjNum--;
                PlaceObjects();
            }

            if (onPlacedObjValid != null)
            {
                onPlacedObjValid();

                benchMark.position = placementPose.position + new Vector3(0, 1.5f, 0);
                benchMark.rotation = placementPose.rotation;
            }
        }
    }

    private void PlaceObjects()
    {
        spawnedObject = Instantiate(randomObjectsArray[Random.Range(0, randomObjectsArray.Length)], placementPose.position, placementPose.rotation);
        placedPrefabObjs.Add(spawnedObject);
        palcedObjNum++;
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
