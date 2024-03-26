using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RafSpecialAttack : MonoBehaviour
{

    PlayerController controller;
    Vector3 start_position;
    BoxCollider2D collider, left_border, right_border;
    private GameObject player_controller1, player_controller2;

    private void Awake()
    {
        start_position = this.transform.position;
        player_controller2 = GameObject.FindGameObjectWithTag("Player_2_Input");
        player_controller1 = GameObject.FindGameObjectWithTag("Player_1_Input");
        controller = GetComponent<PlayerController>();

        collider = GetComponent<BoxCollider2D>();
        left_border = GameObject.FindGameObjectWithTag("Left_Border").GetComponent<BoxCollider2D>();
        right_border = GameObject.FindGameObjectWithTag("Right_Border").GetComponent<BoxCollider2D>();

    }

    public void raf_special_attack()
    {
        
        player_controller1.GetComponent<PlayerInputHandler>().player_input.defaultActionMap = "empty";
        player_controller2.GetComponent<PlayerInputHandler>().player_input.defaultActionMap = "empty";

        collider.isTrigger = true;
        left_border.enabled = false;
        right_border.enabled = false;


        controller.anim.SetInteger("AnimState", 4);
        StartCoroutine(move_bike());  
    }
    
    private IEnumerator move_bike()
    {
        if (this.gameObject.tag == "Player")
        {
            this.gameObject.transform.position += new Vector3(-0.2f, 0, 0);
            controller.anim.SetInteger("AnimState", 4);
            yield return new WaitForSeconds(0.002f);
            StartCoroutine(move_bike());
        }

        if (this.gameObject.tag == "Player_2")
        {
            this.gameObject.transform.position += new Vector3(0.2f, 0, 0);
            controller.anim.SetInteger("AnimState", 4);
            yield return new WaitForSeconds(0.002f);
            StartCoroutine(move_bike());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(/*other.CompareTag("Player") || other.CompareTag("Player") || */other.CompareTag("Destroy_Bike"))
        {
            StopAllCoroutines();

            controller.special_hitbox.enabled = false;
            controller.anim.SetInteger("AnimState", 0);
            this.transform.position = start_position;
            player_controller1.GetComponent<PlayerInputHandler>().player_input.defaultActionMap = "Player";
            player_controller2.GetComponent<PlayerInputHandler>().player_input.defaultActionMap = "Player";
            collider.isTrigger = false;
            left_border.enabled = true;
            right_border.enabled = true;
            
        }
    }


}
