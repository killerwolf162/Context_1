using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    public GameObject player_prefab;
    private PlayerController controller;
    private PlayerInputHandler other_handler;

    [SerializeField]
    Vector3 start_position;

    private void Start()
    {
        if(this.gameObject.tag == "Player_2_Input")
            other_handler = GameObject.FindGameObjectWithTag("Player_1_Input").GetComponent<PlayerInputHandler>();
        if (this.gameObject.tag == "Player_1_Input")
            other_handler = GameObject.FindGameObjectWithTag("Player_2_Input").GetComponent<PlayerInputHandler>();

    }
    public void spawn_player()
    {
        if (other_handler.player_prefab != null)
        controller = GameObject.Instantiate(player_prefab, start_position, transform.rotation).GetComponent<PlayerController>();
    }

    public void OnMove(InputAction.CallbackContext input)
    {
        controller.OnMove(input);
    }

    public void OnPunch(InputAction.CallbackContext input)
    {
        controller.OnPunch(input);
    }

    public void OnKick(InputAction.CallbackContext input)
    {
        controller.OnKick(input);
    }

    public void OnSpecial(InputAction.CallbackContext input)
    {
        controller.OnSpecial(input);
    }

}
