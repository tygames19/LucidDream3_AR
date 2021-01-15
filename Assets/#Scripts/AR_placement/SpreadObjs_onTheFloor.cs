using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SpreadObjs_onTheFloor : MonoBehaviour
{
    #region
    [SerializeField]
    GameObject[] randomObjArray;

    [SerializeField]
    private int maxNum = 0;

    [SerializeField]
    private Vector3 size;

    private int spawnedNum;

    private GameObject spawnedObject;
    private List<GameObject> placedPrefabObjs = new List<GameObject>();
    private bool placementPoseIsValid = false;

    private ARRaycastManager m_RaycastManager;
    private List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    private Pose placementPose;

    [SerializeField]
    private GameObject placementIndicator;

    [SerializeField]
    private Animator glitchAnim;

    [SerializeField]
    private GameObject guideoff;

    [SerializeField]
    private GameObject guideOn;

    [SerializeField]
    private PlayableDirector timeline;

    #endregion

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
            while (spawnedNum < maxNum)
            {
                Vector3 spawnPos = placementPose.position + new Vector3(Random.Range(-size.x, size.x), 0, Random.Range(-size.z, size.z));
                spawnedObject = Instantiate(randomObjArray[Random.Range(0, randomObjArray.Length)], spawnPos, placementPose.rotation);
                spawnedNum++;
                placedPrefabObjs.Add(spawnedObject);
            }
        }

        if (spawnedNum >= maxNum)
        {
            placementIndicator.SetActive(false);
            TimeIsComing();
        }
    }

    private void TimeIsComing()
    {
        guideoff.SetActive(false);
        guideOn.SetActive(true);
        timeline.Play();
        glitchAnim.gameObject.SetActive(true);
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
        var screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
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
