using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TapToPlaceOnPlane : MonoBehaviour
{
    #region Properties

    [SerializeField]
    private GameObject[] randomObjectsArray;

    [SerializeField]
    private int m_MaxNumberOfObjectsToPlace = 1;
    int m_NumberOfPlacedObjects = 0;

    private List<GameObject> placedPrefabObjs = new List<GameObject>();
    private bool placementPoseIsValid = false;

    private GameObject spawnedObject;

    private ARRaycastManager m_RaycastManager;
    private List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    private Pose placementPose;

    private bool isDetectingOn = false;
    private ARPointCloudManager m_pointCloudManager;
    private ARPlaneManager m_planeManager;

    [SerializeField]
    private GameObject placementIndicator;

    [SerializeField]
    private MeshRenderer currentPlane;

    [SerializeField]
    private Material[] mats;  // for changing the material of the plane prefab.
    #endregion

    void Start()
    {
        m_RaycastManager = FindObjectOfType<ARRaycastManager>();
        currentPlane.material = mats[0];
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (m_NumberOfPlacedObjects < m_MaxNumberOfObjectsToPlace)
            {
                isDetectingOn = m_NumberOfPlacedObjects > 0;
                PlaceObjects();
                DisableFeaturePoints();
            }
            else
            {
                Destroy(placedPrefabObjs[0].gameObject);
                placedPrefabObjs.RemoveAt(0);
                m_NumberOfPlacedObjects--;
                PlaceObjects();
            }
        }
    }

    private void DisableFeaturePoints()
    {
        if (isDetectingOn)
        {
            m_pointCloudManager.SetTrackablesActive(false);
            m_pointCloudManager.enabled = false;

            m_planeManager.SetTrackablesActive(false);
            currentPlane.material = mats[1];
        }

    }

    private void PlaceObjects()
    {
        spawnedObject = Instantiate(randomObjectsArray[UnityEngine.Random.Range(0, randomObjectsArray.Length)], placementPose.position, placementPose.rotation);
        placedPrefabObjs.Add(spawnedObject);
        m_NumberOfPlacedObjects++;
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
        var screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f); // Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
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
