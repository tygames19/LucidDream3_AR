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

    [SerializeField]
    private GameObject placementIndicator;

    [SerializeField]
    private GameObject guideOff;

    [SerializeField]
    private GameObject guideOn;

    public ARPlaneManager arPlanemanager
    {
        get => _arPlaneManager;
        set => _arPlaneManager = value;
    }

    [SerializeField]
    private GameObject glitch;

    [SerializeField]
    private GameObject guideTimeline;

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

            guideOn.SetActive(false);
            guideOff.SetActive(true);
            guideTimeline.GetComponent<PlayableDirector>().Play();

            glitch.GetComponent<Animator>().Play("GlitchAnim_custom01");
        }
    }
}
