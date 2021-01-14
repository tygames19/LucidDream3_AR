using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class SpreadObjs_onTheFloor : MonoBehaviour
{
    public GameObject spawnedPrefab;
    public Vector3 size;
    public int maxNum = 0;

    private int spawnedNum;
    private Pose placementPose;
    private GameObject spawnedObject;
    private bool placementPoseIsValid = false;
    private List<GameObject> placedPrefabObjs = new List<GameObject>();

    ARRaycastManager m_RaycastManager;
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    public Camera arCamera;
    public GameObject placementIndicator;

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (placementPoseIsValid && Input.touchCount > 0)
        {
            while (spawnedNum < maxNum)
            {
                Vector3 spawnPos = placementPose.position + new Vector3(Random.Range(-size.x, size.x), 0, Random.Range(-size.z, size.z));
                spawnedObject = Instantiate(spawnedPrefab, spawnPos, placementPose.rotation);
                spawnedNum++;
                placedPrefabObjs.Add(spawnedObject);
            }
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
