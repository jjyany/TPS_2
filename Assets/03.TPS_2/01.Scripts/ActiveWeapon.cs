using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ActiveWeapon : MonoBehaviour
{
    public Transform crossHairTarget;
    public UnityEngine.Animations.Rigging.Rig handIK;
    public UnityEngine.Animations.Rigging.TwoBoneIKConstraint headIK;
    public Transform weaponParent;
    public Transform weaponRightGrip;
    public Transform weaponLeftGrip;
    public Transform weaponHeadGrip;
    public Animator rigController;

    RaycastWeapon weapon;
    void Start()
    {

        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();

        if (existingWeapon)
        {
            Equip(existingWeapon);
        }
    }

    private void Update()
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

            weapon.UpdateBullet(Time.deltaTime); //시간에 따라 총알은 계속 업데이트 되야한다.

            if (Input.GetMouseButtonUp(0))
            {
                weapon.StopFiring();
            }

            if(Input.GetKeyDown(KeyCode.X))
            {
                bool isHolstered = rigController.GetBool("holster_weapon");
                rigController.SetBool("holster_weapon", !isHolstered);
            }
        }
    }

    public void Equip(RaycastWeapon newWeapon)
    {
        if(weapon)
        {
            Destroy(weapon.gameObject);
        }

        weapon = newWeapon;
        weapon.raycastDestination = crossHairTarget;
        handIK.weight = 1.0f;
        headIK.weight = 1.0f;
        weapon.transform.parent = weaponParent;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        rigController.Play("equip_" + weapon.weaponName);
    }


}
