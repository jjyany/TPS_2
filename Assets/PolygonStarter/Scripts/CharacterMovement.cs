using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Animator anim;
    private Vector2 input;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        anim.SetFloat("InputX", input.x);
        anim.SetFloat("InputY", input.y);
    }
}
