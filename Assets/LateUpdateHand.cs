using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateUpdateHand : MonoBehaviour
{
    public Transform targetToFollow;

    void LateUpdate()
    {
        transform.position = targetToFollow.position;
        transform.rotation = targetToFollow.rotation;
    }
}
