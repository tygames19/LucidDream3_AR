using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceObjectsOnPlane : MonoBehaviour
{
    //[SerializeField]
    //[Tooltip("Instantiates this prefab on a plane at the touch location.")]
    //GameObject m_PlacedPrefab;

    ///// <summary>
    ///// The prefab to instantiate on touch.
    ///// </summary>
    //public GameObject placedPrefab
    //{
    //    get { return m_PlacedPrefab; }
    //    set { m_PlacedPrefab = value; }
    //}

    public GameObject[] randomObjectsArray;
    public GameObject placementIndicator;
    private List<GameObject> placedPrefabObjs = new List<GameObject>();
    private bool placementPoseIsValid = false;
    private Pose placementPose;
    /// <summary>
    /// The object instantiated as a result of a successful raycast intersection with a plane.
    /// </summary>
    public GameObject spawnedObject { get; private set; }

    /// <summary>
    /// Invoked whenever an object is placed in on a plane.
    /// </summary>
    public static event Action onPlacedObject;

    ARRaycastManager m_RaycastManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    
    [SerializeField]
    int m_MaxNumberOfObjectsToPlace = 1;

    int m_NumberOfPlacedObjects = 0;

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        UpdatePlacementIndicator();

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (placementPoseIsValid && touch.phase == TouchPhase.Began)
            {
                if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    UpdatePlacementPose();

                    if (m_NumberOfPlacedObjects < m_MaxNumberOfObjectsToPlace)
                    {
                        PlaceObjects();
                    }
                    else
                    {
                        //spawnedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                        Destroy(placedPrefabObjs[0].gameObject);
                        placedPrefabObjs.RemoveAt(0);
                        m_NumberOfPlacedObjects--;
                        PlaceObjects();
                    }
                    
                    if (onPlacedObject != null)
                    {
                        onPlacedObject();
                    }
                }
            }
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
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        m_RaycastManager.Raycast(screenCenter, s_Hits, TrackableType.PlaneWithinPolygon);

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
