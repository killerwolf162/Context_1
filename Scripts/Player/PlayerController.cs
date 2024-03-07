using System;
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

    public int max_health;
    public int current_health;

    private BoxCollider2D punch_hitbox;
    private BoxCollider2D kick_hitbox;
    private BoxCollider2D special_hitbox;


    private Vector3 move;

    private void Awake()
    {
        current_health = max_health;
        rig = GetComponent<Rigidbody2D>();

        if (this.gameObject.tag == "Player")
        {
            punch_hitbox = GameObject.Find("Punch1_hitbox").GetComponent<BoxCollider2D>();
            kick_hitbox = GameObject.Find("Kick1_hitbox").GetComponent<BoxCollider2D>();
            special_hitbox = GameObject.Find("Special1_hitbox").GetComponent<BoxCollider2D>();
        }
        if(this.gameObject.tag == "Player_2")
        {
            punch_hitbox = GameObject.Find("Punch2_hitbox").GetComponent<BoxCollider2D>();
            kick_hitbox = GameObject.Find("Kick2_hitbox").GetComponent<BoxCollider2D>();
            special_hitbox = GameObject.Find("Special2_hitbox").GetComponent<BoxCollider2D>();
        }

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

    public void OnPunch(InputAction.CallbackContext input)
    {
        if (input.ReadValue<float>() > 0)
        {
            punch_hitbox.enabled = true;
            Debug.Log("I'm Punshing");
        }

        else
        {
            Debug.Log("I'm not punshing");
            punch_hitbox.enabled = false;
        }
    }

    public void OnKick(InputAction.CallbackContext input)
    {
        if (input.ReadValue<float>() > 0)
        {
            kick_hitbox.enabled = true;
            Debug.Log("I'm kicking");
        }
        else
        {
            Debug.Log("I'm not kicking");
            kick_hitbox.enabled = false;
        }
    }

    public void OnSpecial(InputAction.CallbackContext input)
    {
        if (input.ReadValue<float>() > 0)
        {
            special_hitbox.enabled = true;
            Debug.Log("I'm using special");
        }
        else
        {
            Debug.Log("I'm not using special");
            special_hitbox.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.tag == "Player")
        {
            if (collision.CompareTag("Player_2"))
            {
                Debug.Log("Player 1 has been hit");
                //take_damage(1);
            }
        }
        if(this.gameObject.tag == "Player_2")

        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("Player 2 has been hit");
                //take_damage(1);
            }
        }
    }

    private void take_damage(int damage)
    {
        current_health -= damage;
    }





}

