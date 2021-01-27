using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_SessionOrigin_Scale_Change2 : MonoBehaviour
{
    [SerializeField]
    Camera arCamera;

    // Start is called before the first frame update
    void Start()
    {
        arCamera.farClipPlane = 20;
        this.gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
