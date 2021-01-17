using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Random = UnityEngine.Random;

public class Constellation_spawn : MonoBehaviour
{
    #region Properties

    [SerializeField]
    private GameObject[] constellations;

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
    bool isAlright = false;

    [SerializeField]
    private GameObject placementIndicator;

    public static event Action onStarsValid;

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
            if (!isAlright && onStarsValid != null)
            {
                // spawn the falling cube over time
                StartCoroutine(PlaceObjects());

                onStarsValid();
            }

            isAlright = true;
        }
    }

    IEnumerator PlaceObjects()
    {
        while (true)
        {
            int randomInterval = (int)Random.Range(minInterval, maxInterval);
            Vector3 spawnPos = placementPose.position + new Vector3(Random.Range(-size.x, size.x), Random.Range(0, height), Random.Range(-size.z, size.z));
            Instantiate(constellations[Random.Range(0, constellations.Length)], spawnPos, placementPose.rotation);
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
