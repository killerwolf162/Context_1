using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimSpecialAttack : MonoBehaviour
{

    GameObject player_input1, player_input2;
    GameObject enemy_player;
    PlayerController controller;
    SpriteRenderer sprite_render;
    BoxCollider2D collider;
    Vector3 enemy_position, start_position;

    private void Awake()
    {
        player_input1 = GameObject.FindGameObjectWithTag("Player_1_Input");
        player_input2 = GameObject.FindGameObjectWithTag("Player_2_Input");
        sprite_render = GetComponent<SpriteRenderer>();
        controller = GetComponent<PlayerController>();
    }

    private void Start()
    {
        if (this.gameObject.tag == "Player")
            enemy_player = GameObject.FindGameObjectWithTag("Player_2");

        if (this.gameObject.tag == "Player_2")
            enemy_player = GameObject.FindGameObjectWithTag("Player");

        collider = GetComponent<BoxCollider2D>();
    }

    public void tim_special_attack()
    {
        enemy_position = enemy_player.transform.position;
        start_position = this.transform.position;
        collider.enabled = false;
        
        player_input1.SetActive(false);
        player_input2.SetActive(false);

        if (this.gameObject.tag == "Player")
            this.transform.position = enemy_position + new Vector3(2.5f, 2.35f, 0);
        if (this.gameObject.tag == "Player_2")
            this.transform.position = enemy_position + new Vector3(-2.5f, 2.35f, 0);

        controller.anim.SetInteger("AnimState", 4);
        StartCoroutine(wait_for_anim());
    }

    public IEnumerator wait_for_anim()
    {
        controller.anim.SetInteger("AnimState", 4);
        yield return new WaitForSeconds(1.5f);
        this.transform.position = start_position;
        yield return new WaitForSeconds(0.002f);
        controller.special_hitbox.enabled = false;
        
        collider.enabled = true;
        player_input1.SetActive(true);
        player_input2.SetActive(true);
        StopAllCoroutines();
        
    }

}
