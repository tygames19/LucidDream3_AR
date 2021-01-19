using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Random = UnityEngine.Random;

public class FallingCube : MonoBehaviour
{
    #region Properties

    [SerializeField]
    private GameObject fallingCube;

    [SerializeField]
    private GameObject floor;

    [SerializeField]
    private float minInterval;

    [SerializeField]
    private float maxInterval;

    [SerializeField]
    private int height;

    [SerializeField]
    private Vector3 size;

    ARRaycastManager arRaycastManager;
    List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    Pose placementPose;
    bool placementPoseIsValid = false;
    bool isFloorSet = false;

    [SerializeField]
    private GameObject placementIndicator;

    public static event Action isFallingValid;

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
            if (!isFloorSet)
            {
                // set the position of the floor
                Instantiate(floor, placementPose.position, Quaternion.identity);

                // spawn the falling cube over time
                StartCoroutine(PlaceObjects());
            }

            isFloorSet = true;

            if (isFallingValid != null)
            {
                isFallingValid();
            }
        }
    }

    IEnumerator PlaceObjects()
    {
        while (true)
        {
            int randomInterval = (int)Random.Range(minInterval, maxInterval);
            Vector3 spawnPos = placementPose.position + new Vector3(Random.Range(-size.x, size.x), height, Random.Range(-size.z, size.z));
            Instantiate(fallingCube, spawnPos, placementPose.rotation);
            yield return new WaitForSeconds(randomInterval);
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
