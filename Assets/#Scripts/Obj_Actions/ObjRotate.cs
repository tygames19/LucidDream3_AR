using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRotate : MonoBehaviour
{
    void Start()
    {
        Quaternion rotation2 = Quaternion.Euler(new Vector3(0, targetAngle, 0));
        StartCoroutine(rotateObject(objectToRotate, rotation2, duration));
    }

    bool rotating = false;
    [SerializeField]
    float targetAngle;

    [SerializeField]
    GameObject objectToRotate;

    [SerializeField]
    float duration;

    IEnumerator rotateObject(GameObject gameObjectToMove, Quaternion newRot, float duration)
    {
        if (rotating)
        {
            yield break;
        }
        rotating = true;

        Quaternion currentRot = gameObjectToMove.transform.rotation;

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            gameObjectToMove.transform.rotation = Quaternion.Lerp(currentRot, newRot, counter / duration);
            yield return null;
        }
        rotating = false;
    }
}
