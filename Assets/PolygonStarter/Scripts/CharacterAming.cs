using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAming : MonoBehaviour
{
    public float turnSpeed = 15f;
    public float aimDuration = 0.3f;

    Camera mainCam;

    public Rig aimLayer;
    public Rig headLayer;

    void Start()
    {
        mainCam = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        float yawCamera = mainCam.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.deltaTime);
    }

    private void Update()
    {
        if(Input.GetMouseButton(1))
        {
            aimLayer.weight += Time.deltaTime / aimDuration;
            headLayer.weight += Time.deltaTime / aimDuration;
        }
        else
        {
            aimLayer.weight -= Time.deltaTime / aimDuration;
            headLayer.weight -= Time.deltaTime / aimDuration;
        }
    }
}
