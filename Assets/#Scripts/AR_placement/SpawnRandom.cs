using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Random = UnityEngine.Random;

public class SpawnRandom : MonoBehaviour
{
    #region
    [SerializeField]
    private GameObject spawnedPrefab;

    [SerializeField]
    private GameObject matrixPeople;

    [SerializeField]
    private Vector3 size;

    [SerializeField]
    private int maxNum = 0;

    int spawnedNum;
    int peopleCount = 0;

    GameObject spawnedObject;
    List<GameObject> placedPrefabObjs = new List<GameObject>();
    bool placementPoseIsValid = false;

    ARRaycastManager arRaycastManager;
    List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    Pose placementPose;

    [SerializeField]
    private GameObject placementIndicator;

    public static event Action isMatrixValid;
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
            while (spawnedNum < maxNum)
            {
                Vector3 spawnPos = placementPose.position + new Vector3(Random.Range(-size.x, size.x), 0, Random.Range(-size.z, size.z));
                spawnedObject = Instantiate(spawnedPrefab, spawnPos, placementPose.rotation);
                spawnedNum++;
                placedPrefabObjs.Add(spawnedObject);
            }

            if (peopleCount >= maxNum)
            {
                peopleCount = 0;
            }

            if (isMatrixValid != null)
            {
                isMatrixValid();

                InvokeRepeating("SpawnEvery20sec", 10, 10);
            }
        }
    } 

    void SpawnEvery20sec()
    {
        /// Matrix People Spawn.
        if (peopleCount < maxNum)
        {
            Vector3 mp_pos = placedPrefabObjs[peopleCount].transform.position;
            Quaternion mp_rot = Quaternion.Euler(placedPrefabObjs[peopleCount].transform.rotation.x, Random.Range(0, 180), placedPrefabObjs[peopleCount].transform.rotation.z);
            Instantiate(matrixPeople, mp_pos, mp_rot);
            peopleCount++;
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
