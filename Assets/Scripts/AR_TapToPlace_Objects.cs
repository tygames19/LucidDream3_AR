using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AR_TapToPlace_Objects : MonoBehaviour
{
    public GameObject[] randomObjectsArray;
    public GameObject placementIndicator;
    public int maxPrefabSpawnCount = 0;

    private List<GameObject> placedPrefabObjs = new List<GameObject>();
    private int placedPrefabCount;

    private GameObject spawnedObject;
    private ARRaycastManager m_RaycastManager;
    private Pose placementPose;
    private bool placementPoseIsValid = false;

    // Start is called before the first frame update
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
        var hits = new List<ARRaycastHit>();
        m_RaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);

        placementPoseIsValid = hits.Count > 0;

        if(placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            /// rotate the placement indicator based on the camera direction.
            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
}
