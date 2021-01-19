using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DisablePlaneForMatrix : MonoBehaviour
{
    //[SerializeField]
    //bool _disablePointCloud;

    //public bool disablePointCloud
    //{
    //    get => _disablePointCloud;
    //    set => _disablePointCloud = value;
    //}

    //[SerializeField]
    //bool _disablePlaneRender;

    //public bool disablePlaneRender
    //{
    //    get => _disablePlaneRender;
    //    set => _disablePlaneRender = value;
    //}

    //[SerializeField]
    //ARPointCloudManager _pointCloudManager;

    //public ARPointCloudManager arPointCloudManager
    //{
    //    get => _pointCloudManager;
    //    set => _pointCloudManager = value;
    //}

    //[SerializeField]
    //ARPlaneManager _arPlaneManager;

    //public ARPlaneManager arPlanemanager
    //{
    //    get => _arPlaneManager;
    //    set => _arPlaneManager = value;
    //}

    //[SerializeField]
    //private MeshRenderer currentPlane;

    //[SerializeField]
    //private Material[] mats;

    [Header("Appear at the first touch")]
    [SerializeField]
    private GameObject matrixPeopleSpawn;

    [SerializeField]
    private GameObject guide01;

    [SerializeField]
    private Transform benchMark;

    [Header("Need to turn off")]
    [SerializeField]
    private GameObject placementIndicator;

    [SerializeField]
    private GameObject guide00;

    void OnEnable()
    {
        SpawnRandom.isMatrixValid += isMatrixValid;
    }

    void OnDisable()
    {
        SpawnRandom.isMatrixValid -= isMatrixValid;
    }

    void isMatrixValid()
    {
        guide00.SetActive(false);
        guide01.SetActive(true);

        placementIndicator.SetActive(false);

        matrixPeopleSpawn.SetActive(true);
        matrixPeopleSpawn.transform.SetPositionAndRotation(benchMark.position, benchMark.rotation);
    }
}
