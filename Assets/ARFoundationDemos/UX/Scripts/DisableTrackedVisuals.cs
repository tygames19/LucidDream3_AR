using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DisableTrackedVisuals : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Disables spawned feature points and the ARPointCloudManager")]
    bool m_DisableFeaturePoints;

    public bool disableFeaturePoints
    {
        get => m_DisableFeaturePoints;
        set => m_DisableFeaturePoints = value;
    }

    [SerializeField]
    [Tooltip("Disables spawned planes and ARPlaneManager")]
    bool m_DisablePlaneRendering;

    public bool disablePlaneRendering
    {
        get => m_DisablePlaneRendering;
        set => m_DisablePlaneRendering = value;
    }

    [SerializeField]
    MeshRenderer currentPlane;

    public Material[] mats;  // for changing the material of the plane prefab.

    [SerializeField]
    ARPointCloudManager m_PointCloudManager;

    public ARPointCloudManager pointCloudManager
    {
        get => m_PointCloudManager;
        set => m_PointCloudManager = value;
    }
    
    [SerializeField]
    ARPlaneManager m_PlaneManager;

    public ARPlaneManager planeManager
    {
        get => m_PlaneManager;
        set => m_PlaneManager = value;
    }
    
    void OnEnable()
    {
        PlaceObjectsOnPlane2.onPlacedObject += OnPlacedObject;
    }

    void OnDisable()
    {
        PlaceObjectsOnPlane2.onPlacedObject -= OnPlacedObject;
    }

    void OnPlacedObject()
    {
        if (m_DisableFeaturePoints)
        {
            m_PointCloudManager.SetTrackablesActive(false);
            m_PointCloudManager.enabled = false;
        }

        if (m_DisablePlaneRendering)
        {
            m_PlaneManager.SetTrackablesActive(false);
            //m_PlaneManager.enabled = false;

           currentPlane.material = mats[1];
        }
    }

    void Start()
    {
        currentPlane.material = mats[0];    
    }
}
