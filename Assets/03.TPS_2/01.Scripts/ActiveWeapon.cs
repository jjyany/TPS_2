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

    RaycastWeapon weapon;
    Animator anim;
    AnimatorOverrideController overrides;
    void Start()
    {
        anim = GetComponent<Animator>();
        overrides = anim.runtimeAnimatorController as AnimatorOverrideController;

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
            anim.SetLayerWeight(1, 0.0f);
        }
    }

    private void SetAnimationDelayed()
    {
        overrides["Weapon_anim_Empty"] = weapon.weaponAnimation;
    }

    public void Equip(RaycastWeapon newWeapon)
    {
        if(weapon)
        {
            Destroy(weapon.gameObject);
        }
        weapon = newWeapon;
        weapon.raycastDestination = crossHairTarget;
        weapon.transform.parent = weaponParent;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;

        handIK.weight = 1.0f;
        headIK.weight = 1.0f;
        anim.SetLayerWeight(1, 1.0f);
        Invoke(nameof(SetAnimationDelayed), 0.001f);
    }

    [ContextMenu("Save weapon pose")]
    private void SaveWeaponPose()
    {
        GameObjectRecorder recorder = new GameObjectRecorder(gameObject);
        recorder.BindComponentsOfType<Transform>(weaponParent.gameObject, false);
        recorder.BindComponentsOfType<Transform>(weaponLeftGrip.gameObject, false);
        recorder.BindComponentsOfType<Transform>(weaponRightGrip.gameObject, false);
        recorder.BindComponentsOfType<Transform>(weaponHeadGrip.gameObject, false);
        recorder.TakeSnapshot(0.0f);
        recorder.SaveToClip(weapon.weaponAnimation);
        UnityEditor.AssetDatabase.SaveAssets();
    }
}
