using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class SpawnRandom : MonoBehaviour
{
    public GameObject spawnedPrefab;
    //public Transform center;
    public Vector3 size;
    public int maxNum = 0;

    private int spawnedNum;
    private Pose placementPose;
    private GameObject spawnedObject;
    private bool placementPoseIsValid = false;
    private List<GameObject> placedPrefabObjs = new List<GameObject>();
    private int peopleCount = 0;

    ARRaycastManager m_RaycastManager;
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    public Camera arCamera;
    public GameObject matrixPeople;
    public GameObject placementIndicator;


    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    void Start()
    {
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
        }

        //while (spawnedNum < maxNum)
        //{
        //    Vector3 spawnPos = placementPose.position + new Vector3(Random.Range(-size.x, size.x), 0, Random.Range(-size.z, size.z));
        //    spawnedObject = Instantiate(spawnedPrefab, spawnPos, placementPose.rotation);
        //    spawnedNum++;
        //    placedPrefabObjs.Add(spawnedObject);
        //}

        if (spawnedNum >= maxNum)
        {
            placementIndicator.SetActive(false);
        }

        if (peopleCount >= maxNum)
        {
            peopleCount = 0;
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
        // matrixPeople.transform.position = placementPose.position + new Vector3(0, 0, 2);
        // matrixPeople.transform.rotation = placementPose.rotation;
    }

    //public void placeRandom()
    //{
    //    ///will get the middle of the screen
    //    //Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane+5));
    //    //Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), Camera.main.farClipPlane / 2));
    //    Vector3 pos = center.position + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z));
    //    spawnedObject = Instantiate(spawnedPrefab, pos, Quaternion.identity);
    //    spawnedNum++;
    //}

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
        var screenCenter = arCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        m_RaycastManager.Raycast(screenCenter, s_Hits, TrackableType.PlaneWithinPolygon);

        placementPoseIsValid = s_Hits.Count > 0;

        if (placementPoseIsValid)
        {
            placementPose = s_Hits[0].pose;

            /// rotate the placement indicator based on the camera direction.
            var cameraForward = arCamera.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
}
