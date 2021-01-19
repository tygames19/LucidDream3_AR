using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.XR.ARFoundation;

public class DisablePlaneForSpread : MonoBehaviour
{
    [SerializeField]
    bool _disablePointCloud;

    public bool disablePointCloud
    {
        get => _disablePointCloud;
        set => _disablePointCloud = value;
    }

    [SerializeField]
    bool _disablePlaneRender;

    public bool disablePlaneRender
    {
        get => _disablePlaneRender;
        set => _disablePlaneRender = value;
    }

    [SerializeField]
    ARPointCloudManager _pointCloudManager;

    public ARPointCloudManager arPointCloudManager
    {
        get => _pointCloudManager;
        set => _pointCloudManager = value;
    }

    [SerializeField]
    ARPlaneManager _arPlaneManager;

    public ARPlaneManager arPlanemanager
    {
        get => _arPlaneManager;
        set => _arPlaneManager = value;
    }

    [SerializeField]
    private Transform benchMark;

    [Header("Appear at the first touch")]
    [SerializeField]
    private GameObject glitch;

    [SerializeField]
    private GameObject bodyParts;

    [SerializeField]
    private GameObject guide01;

    [Header("Need to turn off")]
    [SerializeField]
    private GameObject placementIndicator;

    [SerializeField]
    private GameObject guide02;

    void OnEnable()
    {
        SpreadObjs_onTheFloor.onSpawnObjValid += onSpawnObjValid;
    }

    void OnDisable()
    {
        SpreadObjs_onTheFloor.onSpawnObjValid -= onSpawnObjValid;
    }

    void onSpawnObjValid()
    {
        if (_disablePointCloud)
        {
            _pointCloudManager.SetTrackablesActive(false);
            _pointCloudManager.enabled = false;

            placementIndicator.SetActive(false);
        }

        if (_disablePlaneRender)
        {
            _arPlaneManager.SetTrackablesActive(false);
            _arPlaneManager.enabled = false;

            guide02.SetActive(false);
            guide01.SetActive(true);

            bodyParts.SetActive(true);
            bodyParts.transform.SetPositionAndRotation(benchMark.position, benchMark.rotation);
            glitch.GetComponent<Animator>().Play("GlitchAnim_custom01");
        }
    }
}
