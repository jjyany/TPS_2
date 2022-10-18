using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairTarget : MonoBehaviour
{
    //레이캐스트가 되는 오브젝트의 위치값을 받아온다
    Camera mainCamera;

    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update
    void Awake()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ray.origin = mainCamera.transform.position;
        ray.direction = mainCamera.transform.forward;
        if(Physics.Raycast(ray, out hit))
        {
            transform.position = hit.point;
        }
        else
        {
            transform.position = ray.origin + ray.direction * 1000.0f;
        }
    }
}
