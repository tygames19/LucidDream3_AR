using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible_onWake : MonoBehaviour
{
    void Start()
    {
        this.gameObject.SetActive(false);
    }
}
