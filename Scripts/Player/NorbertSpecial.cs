using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NorbertSpecial : MonoBehaviour
{
    private GameObject player_controller1, player_controller2;
    GameObject enemy_player;
    PlayerController controller;
    Vector3 enemy_position, start_position;

    private void Awake()
    {
        player_controller2 = GameObject.FindGameObjectWithTag("Player_2_Input");
        player_controller1 = GameObject.FindGameObjectWithTag("Player_1_Input");
        controller = GetComponent<PlayerController>();
    }
    private void Start()
    {
        if (this.gameObject.tag == "Player")
            enemy_player = GameObject.FindGameObjectWithTag("Player_2");

        if (this.gameObject.tag == "Player_2")
            enemy_player = GameObject.FindGameObjectWithTag("Player");
    }

    public void norbert_special_attack()
    {

        enemy_position = enemy_player.transform.position;
        start_position = this.transform.position;
        player_controller1.GetComponent<PlayerInputHandler>().player_input.defaultActionMap = "empty";
        player_controller2.GetComponent<PlayerInputHandler>().player_input.defaultActionMap = "empty";

        if (this.gameObject.tag == "Player")
            this.transform.position = enemy_position + new Vector3(10, 0, 0);
        if (this.gameObject.tag == "Player_2")
            this.transform.position = enemy_position + new Vector3(-10, 0, 0);

        controller.anim.SetInteger("AnimState", 4);
        StartCoroutine(wait_for_anim());
    }

    public IEnumerator wait_for_anim()
    {
        yield return new WaitForSeconds(1.6667f);
        controller.anim.SetInteger("AnimState", 0);
        controller.special_hitbox.enabled = false;
        this.transform.position = start_position;
        player_controller1.GetComponent<PlayerInputHandler>().player_input.defaultActionMap = "Player";
        player_controller2.GetComponent<PlayerInputHandler>().player_input.defaultActionMap = "Player";
        StopAllCoroutines();

    }
}
