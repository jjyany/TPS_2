using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAming : MonoBehaviour
{
    public float turnSpeed = 15f;
    public float aimDuration = 0.3f;

    Camera mainCam;
    RaycastWeapon weapon;


    void Start()
    {
        mainCam = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        weapon = GetComponentInChildren<RaycastWeapon>();

    }

    void FixedUpdate()
    {
        float yawCamera = mainCam.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.deltaTime);
    }


    private void LateUpdate()
    {
        //if (weapon)
        //{
        //    if (Input.GetMouseButton(1))
        //    {
        //        if (Input.GetMouseButtonDown(0))
        //        {
        //            weapon.StartFiring();
        //        }
        //        if (weapon.isFiring)
        //        {
        //            weapon.UpdateFireing(Time.deltaTime);
        //        }
        //    }

        //    if (Input.GetMouseButtonUp(0))
        //    {
        //        weapon.StopFiring();
        //    }

        //    weapon.UpdateBullet(Time.deltaTime); //시간에 따라 총알은 계속 업데이트 되야한다.
        //}
    }
}
