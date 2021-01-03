using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARAnchorManager))]
public class AR_TapToPlace_Objects : MonoBehaviour
{
    public GameObject[] randomObjectsArray;
    public GameObject placementIndicator;
    public int maxPrefabSpawnCount = 0;

    private List<GameObject> placedPrefabObjs = new List<GameObject>();
    private List<ARAnchor> m_anchorReferences = new List<ARAnchor>();
    private List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    private int placedPrefabCount;

    private GameObject spawnedObject;
    private ARRaycastManager m_RaycastManager;
    private ARAnchorManager m_ARAnchorManager;
    private ARPlaneManager m_PlaneManager;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    void Start()
    {
        m_RaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (placedPrefabCount < maxPrefabSpawnCount)
            {
                PlaceObjects();
            }
            else
            {
                Destroy(placedPrefabObjs[0].gameObject);
                placedPrefabObjs.RemoveAt(0);
                placedPrefabCount--;

                PlaceObjects();
            }
        }
    }

    private void PlaceObjects()
    {
        spawnedObject = Instantiate(randomObjectsArray[Random.Range(0, randomObjectsArray.Length)], placementPose.position, placementPose.rotation);
        placedPrefabObjs.Add(spawnedObject);
        placedPrefabCount++;

        TrackableId planeId = s_Hits[0].trackableId;
        var anchor = m_ARAnchorManager.AddAnchor(new Pose(placementPose.position, Quaternion.identity));

        if (anchor != null)
        {
            m_anchorReferences.Add(anchor);
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
