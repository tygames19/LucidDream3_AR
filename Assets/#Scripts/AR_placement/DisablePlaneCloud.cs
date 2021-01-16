using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DisablePlaneCloud : MonoBehaviour
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
    private MeshRenderer currentPlane;

    [SerializeField]
    private Material[] mats;

    [SerializeField]
    private GameObject paperPlanes;

    void OnEnable()
    {
        TapToPlaceOnPlane.onPlacedObjValid += onPlacedObjValid;
    }

    void OnDisable()
    {
        TapToPlaceOnPlane.onPlacedObjValid -= onPlacedObjValid;
    }

    void onPlacedObjValid()
    {
        if (_disablePointCloud)
        {
            _pointCloudManager.SetTrackablesActive(false);
            _pointCloudManager.enabled = false;
        }

        if (_disablePlaneRender)
        {
            _arPlaneManager.SetTrackablesActive(false);
            currentPlane.material = mats[1];

            paperPlanes.SetActive(true);
        }
    }

    void Start()
    {
        currentPlane.material = mats[0];
    }
}
