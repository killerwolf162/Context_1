using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{

    //movement
    [SerializeField]
    private float move_speed;


    //physics
    private Rigidbody2D rig;
    private Vector2 move_input;


    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        
        rig.velocity = move_input * move_speed;
    }

    public void OnMove(InputValue input_value)
    {
        
        move_input = input_value.Get<Vector2>();
    }



}

