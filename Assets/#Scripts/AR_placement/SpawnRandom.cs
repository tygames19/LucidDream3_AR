using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

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

    private int spawnedNum;
    private int peopleCount = 0;

    private GameObject spawnedObject;
    private List<GameObject> placedPrefabObjs = new List<GameObject>();
    private bool placementPoseIsValid = false;

    private ARRaycastManager m_RaycastManager;
    private List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    private Pose placementPose;

    [SerializeField]
    private GameObject placementIndicator;
    #endregion

    void Start()
    {
        m_RaycastManager = FindObjectOfType<ARRaycastManager>();
        InvokeRepeating("SpawnEvery20sec", 120, 10);
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            while (spawnedNum < maxNum)
            {
                Vector3 spawnPos = placementPose.position + new Vector3(Random.Range(-size.x, size.x), 0, Random.Range(-size.z, size.z));
                spawnedObject = Instantiate(spawnedPrefab, spawnPos, placementPose.rotation);
                spawnedNum++;
                placedPrefabObjs.Add(spawnedObject);
            }

            if (spawnedNum >= maxNum)
            {
                placementIndicator.SetActive(false);
            }

            if (peopleCount >= maxNum)
            {
                peopleCount = 0;
            }
        }
    } 

    void SpawnEvery20sec()
    {
        /// Matrix People Spawn.
        /// 
        if (peopleCount < maxNum)
        {
            Vector3 mp_pos = placedPrefabObjs[peopleCount].transform.position;
            Quaternion mp_rot = Quaternion.Euler(placedPrefabObjs[peopleCount].transform.rotation.x, Random.Range(0, 180), placedPrefabObjs[peopleCount].transform.rotation.z);
            Instantiate(matrixPeople, mp_pos, mp_rot);
            peopleCount++;
        }
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f); //Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        m_RaycastManager.Raycast(screenCenter, s_Hits, TrackableType.Planes);

        placementPoseIsValid = s_Hits.Count > 0;

        if (placementPoseIsValid)
        {
            placementPose = s_Hits[0].pose;

            /// rotate the placement indicator based on the camera direction.
            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
}
