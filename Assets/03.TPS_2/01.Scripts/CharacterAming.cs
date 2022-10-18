using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAming : MonoBehaviour
{
    public float turnSpeed = 15f;
    public float aimDuration = 0.3f;

    public Rig aimLayer;
    public Rig headLayer;

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
    

    private void Update()
    {
        if(Input.GetMouseButton(1))
        {
            aimLayer.weight += Time.deltaTime / aimDuration;
            headLayer.weight += Time.deltaTime / aimDuration;

            if (Input.GetMouseButtonDown(0))
            {
                weapon.StartFiring();
            }


            if(weapon.isFiring)
            {
                weapon.UpdateFireing(Time.deltaTime);
            }

        }
        else
        {
            aimLayer.weight -= Time.deltaTime / aimDuration;
            headLayer.weight -= Time.deltaTime / aimDuration;
        }

        if(Input.GetMouseButtonUp(0))
        {
            weapon.StopFiring();
        }

        weapon.UpdateBullet(Time.deltaTime); //시간에 따라 총알은 계속 업데이트 되야한다.
    }
}
