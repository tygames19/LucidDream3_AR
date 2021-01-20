using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Random = UnityEngine.Random;

public class SpawnRandom : MonoBehaviour
{
    #region
    [Header ("Matrix Spawn Obj")]
    [SerializeField]
    private GameObject matrixPillar;

    [SerializeField]
    private GameObject matrixPeople;

    [Header("Spawn size and Max Number")]
    [SerializeField]
    private Vector3 size;

    [SerializeField]
    private int maxNum = 0;

    int pillarNum = 0;
    int peopleNum = 0;

    GameObject pillarSpawn;
    GameObject peopleSpawn;
    List<GameObject> matrixPillarList = new List<GameObject>();
    List<GameObject> matrixPeopleList = new List<GameObject>();

    bool placementPoseIsValid = false;
    bool benchMarkIsSet = false;

    ARRaycastManager arRaycastManager;
    List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    Pose placementPose;

    [Header("Need to Turn off")]
    [SerializeField]
    private GameObject placementIndicator;

    [SerializeField]
    private Transform benchMark;

    public static event Action isMatrixValid;
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
            while (pillarNum < maxNum)
            {
                Vector3 spawnPos = placementPose.position + new Vector3(Random.Range(-size.x, size.x), 0, Random.Range(0, size.z));
                pillarSpawn = Instantiate(matrixPillar, spawnPos, placementPose.rotation);
                matrixPillarList.Add(pillarSpawn);
                pillarNum++;
            }

            if (peopleNum >= maxNum)
            {
                peopleNum = 0;
            }

            if (isMatrixValid != null)
            {
                isMatrixValid();
                InvokeRepeating("SpawnEvery20sec", 8, 8);

                if (!benchMarkIsSet)
                {
                    benchMark.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
                    benchMarkIsSet = true;
                }
            }
        }
    }

    void SpawnEvery20sec()
    {
        /// Matrix People Spawn.
        if (peopleNum < maxNum)
        {
            Vector3 mp_pos = matrixPillarList[peopleNum].transform.position;
            Quaternion mp_rot = Quaternion.Euler(matrixPillarList[peopleNum].transform.rotation.x, Random.Range(0, 180), matrixPillarList[peopleNum].transform.rotation.z);
            peopleSpawn = Instantiate(matrixPeople, mp_pos, mp_rot);
            matrixPeopleList.Add(peopleSpawn);
            peopleNum++;
        }

        if (peopleNum > 2)
        {
            Destroy(matrixPeopleList[0]);
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
