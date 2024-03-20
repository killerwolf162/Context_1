using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoeriSpecialAttack : MonoBehaviour
{


    GameObject player_input1, player_input2;
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
        player_input1 = GameObject.FindGameObjectWithTag("Player_1_Input");
        player_input2 = GameObject.FindGameObjectWithTag("Player_2_Input");
        controller = GetComponent<PlayerController>();
        collider = GetComponent<BoxCollider2D>();
        if (this.gameObject.tag == "Player")
            enemy_player = GameObject.FindGameObjectWithTag("Player_2");

        if (this.gameObject.tag == "Player_2")
            enemy_player = GameObject.FindGameObjectWithTag("Player");

        scale_change = new Vector3(0.08f, 0.08f, 0);
        position_change = new Vector3(0, 0.04f, 0);
    }

    public void joeri_special_attack()
    {
        enemy_position = enemy_player.transform.position;
        start_position = this.transform.position;
        player_input1.SetActive(false);
        player_input2.SetActive(false);

        controller.idle_timer = 0;
        controller.anim.SetInteger("AnimState", 4);

        Instantiate(word_1, enemy_position + new Vector3(4, 20, 0), transform.rotation);
        Instantiate(word_2, enemy_position + new Vector3(-4, 25, 0), transform.rotation);
        Instantiate(word_3, enemy_position + new Vector3(0, 30, 0), transform.rotation);

        word_1_to_move = GameObject.FindGameObjectWithTag("Word_1");
        word_2_to_move = GameObject.FindGameObjectWithTag("Word_2");
        word_3_to_move = GameObject.FindGameObjectWithTag("Word_3");

        collider.enabled = false;

        StartCoroutine(falling_words());
    }

    private IEnumerator falling_words()
    {
        enemy_position = transform.position;
        controller.idle_timer = 0;
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

        this.transform.localScale += scale_change;
        this.transform.position += position_change;

        if (word_1_to_move == null & word_2_to_move == null & word_3_to_move == null)
        {
            StopAllCoroutines();
            controller.special_hitbox.enabled = false;
            controller.anim.SetInteger("AnimState", 0);
            player_input1.SetActive(true);
            player_input2.SetActive(true);
            this.transform.localScale = new Vector3(10, 10, 0);
            this.transform.position = start_position;
            collider.enabled = true;
        }
        else
        {
            yield return new WaitForSeconds(0.01f);
            StartCoroutine(falling_words());
        }
            

    }
}
