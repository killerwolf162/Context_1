using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    //movement
    [SerializeField]
    private float move_speed;
    private Vector2 move_input;
    private Rigidbody2D rig;

    private Vector3 move;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rig.transform.Translate(move * Time.deltaTime * move_speed);
    }

    public void OnMove(InputAction.CallbackContext input_value)
    {
        Vector2 movement = input_value.ReadValue<Vector2>();
        move = new Vector3(movement.x, 0, movement.y);
    } 



}

