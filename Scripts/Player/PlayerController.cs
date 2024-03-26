using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    private GameObject player_controller;

    public Animator anim;

    PlayerController other_player;

    private RafSpecialAttack raf_special;
    private JoeriSpecialAttack joeri_special;
    private TimSpecialAttack tim_special;
    private NorbertSpecial norbert_special;

    [SerializeField]
    public float cool_down_timer = 0;

    private Vector3 move;

    private void Awake()
    {
        current_health = max_health;
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        if (this.gameObject.name == "Raf_1(Clone)" || this.gameObject.name == "Raf_2(Clone)")
        {
            raf_special = GetComponent<RafSpecialAttack>();
        }
        if (this.gameObject.name == "Joeri_1(Clone)" || this.gameObject.name == "Joeri_2(Clone)")
        {
            joeri_special = GetComponent<JoeriSpecialAttack>();
        }
        if (this.gameObject.name == "Tim_1(Clone)" || this.gameObject.name == "Tim_2(Clone)")
        {
            tim_special = GetComponent<TimSpecialAttack>();
        }
        if (this.gameObject.name == "Norbert_1(Clone)" || this.gameObject.name == "Norbert_2(Clone)")
        {
            norbert_special = GetComponent<NorbertSpecial>();
        }

        if (this.gameObject.tag == "Player")
        {
            other_player = GameObject.FindGameObjectWithTag("Player_2").GetComponent<PlayerController>();
            punch_hitbox = GameObject.Find("Punch1_hitbox").GetComponent<BoxCollider2D>();
            kick_hitbox = GameObject.Find("Kick1_hitbox").GetComponent<BoxCollider2D>();
            special_hitbox = GameObject.Find("Special1_hitbox").GetComponent<BoxCollider2D>();
            health_bar = GameObject.FindGameObjectWithTag("HealthBar_P1").GetComponent<HealthBar>();
            special_bar = GameObject.FindGameObjectWithTag("SpecialBar_P1").GetComponent<SpecialBar>();
            player_controller = GameObject.FindGameObjectWithTag("Player_1_Input");
        }
        if (this.gameObject.tag == "Player_2")
        {
            other_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            punch_hitbox = GameObject.Find("Punch2_hitbox").GetComponent<BoxCollider2D>();
            kick_hitbox = GameObject.Find("Kick2_hitbox").GetComponent<BoxCollider2D>();
            special_hitbox = GameObject.Find("Special2_hitbox").GetComponent<BoxCollider2D>();
            health_bar = GameObject.FindGameObjectWithTag("HealthBar_P2").GetComponent<HealthBar>();
            special_bar = GameObject.FindGameObjectWithTag("SpecialBar_P2").GetComponent<SpecialBar>();
            player_controller = GameObject.FindGameObjectWithTag("Player_2_Input");
        }

        special_bar.set_special(current_special);
        health_bar.set_max_health(max_health);
    }

    private void FixedUpdate()
    {
        rig.transform.Translate(move * Time.deltaTime * move_speed);

        if ( move.x != 0 && cool_down_timer < 0.8)
        {
            anim.SetInteger("AnimState", 1);
        }
        if( move.x == 0 && cool_down_timer < 0.8)
        {
            anim.SetInteger("AnimState", 0);
        }

        if ( cool_down_timer > 0)
        {

            cool_down_timer -= Time.deltaTime;
        }

        if( cool_down_timer > 0.8f)
        {
            player_controller.GetComponent<PlayerInputHandler>().player_input.defaultActionMap = "empty";
        }
        else if(cool_down_timer < 0.8f)
        {
            player_controller.GetComponent<PlayerInputHandler>().player_input.defaultActionMap = "Player";
        }

        if( current_health <= 0)
        {

            if(this.gameObject.tag == "Player")
            {
                Destroy(gameObject);
                SceneManager.LoadScene("Player_1_died");
            }
            else if (this.gameObject.tag == "Player_2")
            {
                Destroy(gameObject);
                SceneManager.LoadScene("Player_2_died");
            }
        }
    }

    public void OnMove(InputAction.CallbackContext input_value)
    {

        Vector2 movement = input_value.ReadValue<Vector2>();

        move = new Vector3(movement.x, 0, movement.y);

    }

    public void OnPunch(InputAction.CallbackContext input)
    {
        if (input.ReadValue<float>() > 0 && cool_down_timer <= 0.8f)
        {
            punch_hitbox.enabled = true;

            anim.SetInteger("AnimState", 2);
            cool_down_timer = 1; 
        }

        StartCoroutine(disable_hitbox_delay());
    }

    public void OnKick(InputAction.CallbackContext input)
    {
        if (input.ReadValue<float>() > 0 && cool_down_timer <= 0.8f)
        {
            kick_hitbox.enabled = true;
            
            anim.SetInteger("AnimState", 3);
            cool_down_timer = 1;
        }

        StartCoroutine(disable_hitbox_delay());
    }

    public void OnSpecial(InputAction.CallbackContext input)
    {

        if (input.ReadValue<float>() > 0 && cool_down_timer <= 0.8f && current_special == 3)
        {
            

            if (raf_special != null)
            {
                anim.SetInteger("AnimState", 4);
                raf_special.raf_special_attack();
            }

            if (joeri_special != null)
            {
                joeri_special.joeri_special_attack();
            }

            else if (tim_special != null)
            {
                tim_special.tim_special_attack();
            }

            else if (norbert_special != null)
            {
                norbert_special.norbert_special_attack();
            }

            cool_down_timer = 1;
            current_special = 0;
            special_bar.set_special(current_special);
            special_hitbox.enabled = true;
        }
        disable_hitbox_delay();


    }

    private IEnumerator disable_hitbox_delay()
    {
        yield return new WaitForSeconds(0.1f);
        kick_hitbox.enabled = false;
        punch_hitbox.enabled = false;
        special_hitbox.enabled = false;
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

