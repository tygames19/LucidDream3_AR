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
    private GameObject[] randomObjectsArray; // need to shuffle and not repeat

    [SerializeField]
    private int maxNumForSpawn = 1;

    private int placedPrefab = 0;

    List<GameObject> placedPrefabObjs = new List<GameObject>();
    bool placementPoseIsValid = false;
    bool benchMarkIsSet = false;

    GameObject spawnedObject;

    ARRaycastManager arRaycastManager;
    List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    Pose placementPose;

    [SerializeField]
    private GameObject placementIndicator;

    [SerializeField]
    private Transform benchMark;

    // Shuffle random number without repeating
    //int[] arrayNum = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }; // = randomobjectsArray number
    //List<int> forShuffle = null;

    public static event Action onPlacedObjValid;

    #endregion

    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        placementIndicator.SetActive(false);
        benchMarkIsSet = false;
        //forShuffle.AddRange(arrayNum);
    }

    void Update()
    {
        UpdatePlacementPose();

        if (placementPoseIsValid == true && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

            if (placedPrefab < maxNumForSpawn)
            {
                PlaceObjects();
            }
            else
            {
                // 맥스 이상되면 지워라.
                Destroy(placedPrefabObjs[0].gameObject);
                placedPrefabObjs.RemoveAt(0);
                placedPrefab--;
            }

            // activate at the first touch 
            if (onPlacedObjValid != null && benchMarkIsSet == false)
            {
                onPlacedObjValid();

                if (!benchMarkIsSet)
                {
                    benchMark.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
                    benchMarkIsSet = true;
                }
            }
        }
    }

    private void PlaceObjects()
    {
        spawnedObject = Instantiate(randomObjectsArray[Random.Range(0, randomObjectsArray.Length)], placementPose.position, placementPose.rotation);
        placedPrefabObjs.Add(spawnedObject);
        placedPrefab++;
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

    //int GetUniqueRandom(bool reloadEmptyList)
    //{
    //    if (forShuffle.Count == 0)
    //    {
    //        if (reloadEmptyList)
    //        {
    //            forShuffle.AddRange(arrayNum);
    //        }
    //        else
    //        {
    //            return -1; // finite loop. 
    //        }
    //    }

    //    int rand = Random.Range(0, forShuffle.Count);
    //    int value = forShuffle[rand];
    //    forShuffle.RemoveAt(rand);
    //    return value;
    //}
}
