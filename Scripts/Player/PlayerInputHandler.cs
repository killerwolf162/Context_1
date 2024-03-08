using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject player_prefab;
    private PlayerController controller;

    [SerializeField]
    Vector3 start_position;

    private void Awake()
    {


        if (player_prefab != null)
        {
            controller = GameObject.Instantiate(player_prefab, start_position, transform.rotation).GetComponent<PlayerController>();
        }
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