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
    public HealthBar health_bar;

    private BoxCollider2D punch_hitbox;
    private BoxCollider2D kick_hitbox;
    private BoxCollider2D special_hitbox;

    private float cool_down_timer = 0;

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
            health_bar = GameObject.FindGameObjectWithTag("HealthBar_P1").GetComponent<HealthBar>();
        }
        if(this.gameObject.tag == "Player_2")
        {
            punch_hitbox = GameObject.Find("Punch2_hitbox").GetComponent<BoxCollider2D>();
            kick_hitbox = GameObject.Find("Kick2_hitbox").GetComponent<BoxCollider2D>();
            special_hitbox = GameObject.Find("Special2_hitbox").GetComponent<BoxCollider2D>();
            health_bar = GameObject.FindGameObjectWithTag("HealthBar_P2").GetComponent<HealthBar>();
        }

        
        health_bar.set_max_health(max_health);
    }

    private void FixedUpdate()
    {
        rig.transform.Translate(move * Time.deltaTime * move_speed);

        if( cool_down_timer > 0)
        {
            cool_down_timer -= Time.deltaTime;
        }
    }

    public void OnMove(InputAction.CallbackContext input_value)
    {
        Vector2 movement = input_value.ReadValue<Vector2>();
        move = new Vector3(movement.x, 0, movement.y);
    }

    public void OnPunch(InputAction.CallbackContext input)
    {
        if (input.ReadValue<float>() > 0 && cool_down_timer <= 0)
        {
            punch_hitbox.enabled = true;
            Debug.Log("I'm Punshing");
            cool_down_timer = 1;
        }
        else if(input.ReadValue<float>() > 0 && cool_down_timer > 0)
        {
            Debug.Log("im on cooldown");
        }
        else
        {
            Debug.Log("I'm not punshing");
            punch_hitbox.enabled = false;
        }
    }

    public void OnKick(InputAction.CallbackContext input)
    {
        if (input.ReadValue<float>() > 0 && cool_down_timer <= 0)
        {
            kick_hitbox.enabled = true;
            Debug.Log("I'm kicking");
            cool_down_timer = 1;
        }
        else if (input.ReadValue<float>() > 0 && cool_down_timer > 0)
        {
            Debug.Log("im on cooldown");
        }
        else
        {
            Debug.Log("I'm not kicking");
            kick_hitbox.enabled = false;
        }
    }

    public void OnSpecial(InputAction.CallbackContext input)
    {

        if (input.ReadValue<float>() > 0 && cool_down_timer <= 0)
        {
            special_hitbox.enabled = true;
            Debug.Log("I'm using special");
            cool_down_timer = 1;
        }
        else if (input.ReadValue<float>() > 0 && cool_down_timer > 0)
        {
            Debug.Log("im on cooldown");
        }
        else
        {
            Debug.Log("I'm not using special");
            special_hitbox.enabled = false;
        }
    }

    public void take_damage(int damage)
    {
        if(move.x >= 0)
        {
            current_health -= damage;
            health_bar.set_health(current_health);
        }
        else if(move.x < 0)
        {
            Debug.Log("I blocked");
        }

    }





}

