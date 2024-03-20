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
    private Rigidbody2D rig;

    public int max_health;
    public int current_health;
    public HealthBar health_bar;

    public int max_special;
    public int current_special;
    public SpecialBar special_bar;

    private BoxCollider2D punch_hitbox;
    private BoxCollider2D kick_hitbox;
    public BoxCollider2D special_hitbox;

    private GameObject player_input1, player_input2;

    public Animator anim;

    PlayerController other_player;

    private RafSpecialAttack raf_special;
    private JoeriSpecialAttack joeri_special;

    [SerializeField]
    private float cool_down_timer = 0;
    public float idle_timer = 0f;

    private Vector3 move;

    private void Awake()
    {
        player_input1 = GameObject.FindGameObjectWithTag("Player_1_Input");
        player_input2 = GameObject.FindGameObjectWithTag("Player_2_Input");

        current_health = max_health;
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        if(this.gameObject.name == "Raf_1(Clone)" || this.gameObject.name == "Raf_2(Clone)")
        {
            raf_special = GetComponent<RafSpecialAttack>();
        }
        if (this.gameObject.name == "Joei_1(Clone)" || this.gameObject.name == "Joeri_2(Clone)")
        {
            joeri_special = GetComponent<JoeriSpecialAttack>();
        }
    }

    private void Start()
    {
        if (this.gameObject.tag == "Player")
        {
            other_player = GameObject.FindGameObjectWithTag("Player_2").GetComponent<PlayerController>();
            punch_hitbox = GameObject.Find("Punch1_hitbox").GetComponent<BoxCollider2D>();
            kick_hitbox = GameObject.Find("Kick1_hitbox").GetComponent<BoxCollider2D>();
            special_hitbox = GameObject.Find("Special1_hitbox").GetComponent<BoxCollider2D>();
            health_bar = GameObject.FindGameObjectWithTag("HealthBar_P1").GetComponent<HealthBar>();
            special_bar = GameObject.FindGameObjectWithTag("SpecialBar_P1").GetComponent<SpecialBar>();
        }
        if (this.gameObject.tag == "Player_2")
        {
            other_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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

        idle_timer += Time.deltaTime/2;

        if(idle_timer >= 0.1f)
        {
            if (move.x > 0 || move.x < 0)
                idle_timer = -1;
            if (anim.GetInteger("AnimState") == 4)
                idle_timer = 0;
            else
            {
                anim.SetInteger("AnimState", 0);
                idle_timer = 0;
            }

        }


        if ( cool_down_timer > 0)
        {

            cool_down_timer -= Time.deltaTime;
        }
    }

    public void OnMove(InputAction.CallbackContext input_value)
    {
        idle_timer = -0.2f;
        idle_timer -= Time.deltaTime;
        Vector2 movement = input_value.ReadValue<Vector2>();

        if (move.x < 0)
        {
            anim.SetBool("IsWalkingBackwards", true);
            anim.SetInteger("AnimState", 1);
        }

        if (move.x > 0)
        {
            anim.SetBool("IsWalkingBackwards", false);
            anim.SetInteger("AnimState", 1);
        }

        move = new Vector3(movement.x, 0, movement.y);

    }

    public void OnPunch(InputAction.CallbackContext input)
    {
        if (input.ReadValue<float>() > 0 && cool_down_timer <= 0)
        {
            punch_hitbox.enabled = true;

            idle_timer = -0.25f;
            idle_timer -= Time.deltaTime;
            
            anim.SetInteger("AnimState", 2);
            cool_down_timer = 1; 
        }
        if ((input.ReadValue<float>() <= 0))
        {
            punch_hitbox.enabled = false;
        }


    }

    public void OnKick(InputAction.CallbackContext input)
    {
        if (input.ReadValue<float>() > 0 && cool_down_timer <= 0)
        {
            kick_hitbox.enabled = true;

            idle_timer = -0.2f;
            idle_timer -= Time.deltaTime;
            
            anim.SetInteger("AnimState", 3);
            cool_down_timer = 1;
        }

        if ((input.ReadValue<float>() <= 0))
        {
            kick_hitbox.enabled = false;
        }
    }

    public void OnSpecial(InputAction.CallbackContext input)
    {

        if (input.ReadValue<float>() > 0 && cool_down_timer <= 0 && current_special == 3)
        {
            special_hitbox.enabled = true;

            idle_timer = -0.5f;
            idle_timer -= Time.deltaTime;


            if (raf_special != null)
            {
                anim.SetInteger("AnimState", 4);
                raf_special.raf_special_attack();
            }

            if (joeri_special != null)
            {
                joeri_special.joeri_special_attack();
                this.gameObject.transform.position += new Vector3(0.1f, 0, 0);
            }

            cool_down_timer = 1;
            current_special = 0;
            special_bar.set_special(current_special);
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
            current_special += 1;
            special_bar.set_special(current_special);
        }

    }
}

