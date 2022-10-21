using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ActiveWeapon : MonoBehaviour
{
    public Transform crossHairTarget;
    public UnityEngine.Animations.Rigging.Rig handIK;
    public UnityEngine.Animations.Rigging.Rig headIK;

    RaycastWeapon weapon;
    void Start()
    {
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();

        if (existingWeapon)
        {
            Equip(existingWeapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon)
        {
            if (Input.GetMouseButton(1))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    weapon.StartFiring();
                }
                if (weapon.isFiring)
                {
                    weapon.UpdateFireing(Time.deltaTime);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                weapon.StopFiring();
            }
            weapon.UpdateBullet(Time.deltaTime); //시간에 따라 총알은 계속 업데이트 되야한다.
        }
        else
        {
            handIK.weight = 0.0f;
            headIK.weight = 0.0f;
        }
    }

    public void Equip(RaycastWeapon newWeapon)
    {
        weapon = newWeapon;
        weapon.raycastDestination = crossHairTarget;
    }
}
