using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RafSpecialAttack : MonoBehaviour
{

    GameObject player_input1, player_input2;
    PlayerController controller;
    Vector3 start_position;
    BoxCollider2D collider, left_border, right_border;

    private void Awake()
    {
        player_input1 = GameObject.FindGameObjectWithTag("Player_1_Input");
        player_input2 = GameObject.FindGameObjectWithTag("Player_2_Input");
        controller = GetComponent<PlayerController>();

        collider = GetComponent<BoxCollider2D>();
        left_border = GameObject.FindGameObjectWithTag("Left_Border").GetComponent<BoxCollider2D>();
        right_border = GameObject.FindGameObjectWithTag("Right_Border").GetComponent<BoxCollider2D>();

    }

    public void raf_special_attack()
    {
        start_position = this.transform.position;
        player_input1.SetActive(false);
        player_input2.SetActive(false);

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
            controller.idle_timer = 0;
            this.gameObject.transform.position += new Vector3(-0.2f, 0, 0);
            yield return new WaitForSeconds(0.002f);
            StartCoroutine(move_bike());
        }

        if (this.gameObject.tag == "Player_2")
        {
            controller.idle_timer = 0;
            this.gameObject.transform.position += new Vector3(0.2f, 0, 0);
            yield return new WaitForSeconds(0.002f);
            StartCoroutine(move_bike());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Destroy_Bike"))
        {
            StopAllCoroutines();

            controller.special_hitbox.enabled = false;
            this.transform.position = start_position;
            controller.anim.SetInteger("AnimState", 0);
            player_input1.SetActive(true);
            player_input2.SetActive(true);
            collider.isTrigger = false;
            left_border.enabled = true;
            right_border.enabled = true;
            
        }
    }


}
