using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DisablePlaneForFalling : MonoBehaviour
{
    //[SerializeField]
    //bool _disablePointCloud;

    //public bool disablePointCloud
    //{
    //    get => _disablePointCloud;
    //    set => _disablePointCloud = value;
    //}

    [SerializeField]
    bool _disablePlaneRender;

    public bool disablePlaneRender
    {
        get => _disablePlaneRender;
        set => _disablePlaneRender = value;
    }

    //[SerializeField]
    //ARPointCloudManager _pointCloudManager;

    //public ARPointCloudManager arPointCloudManager
    //{
    //    get => _pointCloudManager;
    //    set => _pointCloudManager = value;
    //}

    [SerializeField]
    ARPlaneManager _arPlaneManager;

    public ARPlaneManager arPlanemanager
    {
        get => _arPlaneManager;
        set => _arPlaneManager = value;
    }

    [SerializeField]
    private GameObject guide01;

    [SerializeField]
    private GameObject guide02;

    void OnEnable()
    {
        FallingCube.isFallingValid += isFallingValid;
    }

    void OnDisable()
    {
        FallingCube.isFallingValid -= isFallingValid;
    }

    void isFallingValid()
    {
        //if (_disablePointCloud)
        //{
        //    _pointCloudManager.SetTrackablesActive(false);
        //    _pointCloudManager.enabled = false;
        //}

        if (_disablePlaneRender)
        {
            _arPlaneManager.SetTrackablesActive(false);
            _arPlaneManager.enabled = false;

            guide01.SetActive(false);
            guide02.SetActive(true);
        }
    }
}
