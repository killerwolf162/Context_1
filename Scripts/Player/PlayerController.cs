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

    public int max_special;
    public int current_special;
    public SpecialBar special_bar;

    private BoxCollider2D punch_hitbox;
    private BoxCollider2D kick_hitbox;
    private BoxCollider2D special_hitbox;

    private Animator anim;

    [SerializeField]
    private float cool_down_timer = 0;
    private float idle_timer = 0f;

    private Vector3 move;

    private void Awake()
    {
        current_health = max_health;
        rig = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        if (this.gameObject.tag == "Player")
        {
            punch_hitbox = GameObject.Find("Punch1_hitbox").GetComponent<BoxCollider2D>();
            kick_hitbox = GameObject.Find("Kick1_hitbox").GetComponent<BoxCollider2D>();
            special_hitbox = GameObject.Find("Special1_hitbox").GetComponent<BoxCollider2D>();
            health_bar = GameObject.FindGameObjectWithTag("HealthBar_P1").GetComponent<HealthBar>();
            special_bar = GameObject.FindGameObjectWithTag("SpecialBar_P1").GetComponent<SpecialBar>();
        }
        if(this.gameObject.tag == "Player_2")
        {
            punch_hitbox = GameObject.Find("Punch2_hitbox").GetComponent<BoxCollider2D>();
            kick_hitbox = GameObject.Find("Kick2_hitbox").GetComponent<BoxCollider2D>();
            special_hitbox = GameObject.Find("Special2_hitbox").GetComponent<BoxCollider2D>();
            health_bar = GameObject.FindGameObjectWithTag("HealthBar_P2").GetComponent<HealthBar>();
            special_bar = GameObject.FindGameObjectWithTag("SpecialBar_P2").GetComponent<SpecialBar>();
        }

        special_bar.set_special(current_special);
        health_bar.set_max_health(max_health);
    }

    private void FixedUpdate()
    {
        rig.transform.Translate(move * Time.deltaTime * move_speed);

        idle_timer += Time.deltaTime;

        if(idle_timer >= 0.5f)
            anim.SetInteger("AnimState", 0);

        if ( cool_down_timer > 0)
        {

            cool_down_timer -= Time.deltaTime;
        }
    }

    public void OnMove(InputAction.CallbackContext input_value)
    {
        idle_timer = 0;
        idle_timer -= Time.deltaTime;
        Vector2 movement = input_value.ReadValue<Vector2>();
        move = new Vector3(movement.x, 0, movement.y);
        anim.SetInteger("AnimState", 1);
    }

    public void OnPunch(InputAction.CallbackContext input)
    {
        if (input.ReadValue<float>() > 0 && cool_down_timer <= 0)
        {
            idle_timer = 0;
            idle_timer -= Time.deltaTime;
            punch_hitbox.enabled = true;
            Debug.Log("I'm Punshing");
            anim.SetInteger("AnimState", 2);
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
            idle_timer = 0;
            idle_timer -= Time.deltaTime;
            kick_hitbox.enabled = true;
            Debug.Log("I'm kicking");
            anim.SetInteger("AnimState", 3);
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

        if (input.ReadValue<float>() > 0 && cool_down_timer <= 0 && current_special >= 3)
        {
            idle_timer = 0;
            idle_timer -= Time.deltaTime;
            special_hitbox.enabled = true;
            Debug.Log("I'm using special");
            anim.SetInteger("AnimState", 4);
            cool_down_timer = 1;
            current_special = 0;
            special_bar.set_special(current_special);
        }
        else if(input.ReadValue<float>() > 0 && current_special < 3)
        {
            Debug.Log("Im dont have enough special progress");
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
            current_special += 1;
            special_bar.set_special(current_special);
        }

    }





}

