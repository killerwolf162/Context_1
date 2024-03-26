using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeriSpecialAttack : MonoBehaviour
{


    private GameObject player_controller1, player_controller2;
    GameObject enemy_player;
    [SerializeField]
    GameObject word_1, word_2, word_3;
    GameObject word_1_to_move, word_2_to_move, word_3_to_move;
    PlayerController controller;
    BoxCollider2D collider;
    Vector3 enemy_position, start_position, scale_change, position_change;

    BoxCollider2D bottom_border;

    private void Awake()
    {
        player_controller2 = GameObject.FindGameObjectWithTag("Player_2_Input");
        player_controller1 = GameObject.FindGameObjectWithTag("Player_1_Input");
        controller = GetComponent<PlayerController>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        if (this.gameObject.tag == "Player")
            enemy_player = GameObject.FindGameObjectWithTag("Player_2");

        if (this.gameObject.tag == "Player_2")
            enemy_player = GameObject.FindGameObjectWithTag("Player");
    }

    public void joeri_special_attack()
    {
        enemy_position = enemy_player.transform.position;
        player_controller1.GetComponent<PlayerInputHandler>().player_input.defaultActionMap = "empty";
        player_controller2.GetComponent<PlayerInputHandler>().player_input.defaultActionMap = "empty";

        controller.anim.SetInteger("AnimState", 4);

        Instantiate(word_1, enemy_position + new Vector3(4, 20, 0), transform.rotation);
        Instantiate(word_2, enemy_position + new Vector3(-4, 25, 0), transform.rotation);
        Instantiate(word_3, enemy_position + new Vector3(0, 30, 0), transform.rotation);

        word_1_to_move = GameObject.FindGameObjectWithTag("Word_1");
        word_2_to_move = GameObject.FindGameObjectWithTag("Word_2");
        word_3_to_move = GameObject.FindGameObjectWithTag("Word_3");


        StartCoroutine(falling_words());
    }

    private IEnumerator falling_words()
    {
        controller.cool_down_timer = 1;
        enemy_position = transform.position;
        if(controller.anim.GetInteger("AnimState") != 4)
        {
            controller.anim.SetInteger("AnimState", 4);
        }
        else
            controller.anim.SetInteger("AnimState", 5);

        if (word_1_to_move != null)
            word_1_to_move.transform.position += new Vector3(0, -0.1f, 0);
        if (word_2_to_move != null)
            word_2_to_move.transform.position += new Vector3(0, -0.1f, 0);
        if (word_3_to_move != null)
            word_3_to_move.transform.position += new Vector3(0, -0.1f, 0);

        if (word_1_to_move == null & word_2_to_move == null & word_3_to_move == null)
        {
            StopAllCoroutines();
            controller.special_hitbox.enabled = false;
            controller.anim.SetInteger("AnimState", 0);
            player_controller1.GetComponent<PlayerInputHandler>().player_input.defaultActionMap = "Player";
            player_controller2.GetComponent<PlayerInputHandler>().player_input.defaultActionMap = "Player";
        }
        else
        {
            yield return new WaitForSeconds(0.01f);
            StartCoroutine(falling_words());
        }
            

    }
}
