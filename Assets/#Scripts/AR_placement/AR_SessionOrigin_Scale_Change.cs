using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_SessionOrigin_Scale_Change : MonoBehaviour
{
    [SerializeField]
    Camera arCamera;

    // Start is called before the first frame update
    void Start()
    {
        arCamera.farClipPlane = 40;
        this.gameObject.transform.localScale = new Vector3(5, 5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
