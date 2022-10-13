using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAming : MonoBehaviour
{
    public float turnSpeed = 15f;
    Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float yawCamera = mainCam.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.deltaTime);
    }
}
