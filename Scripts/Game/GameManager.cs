using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public List<PlayerInput> player_list = new List<PlayerInput>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {

    }

    private void OnPlayerJoined(PlayerInput player_input)
    {
        
    }

    void OnPlayerLeft(PlayerInput player_input)
    {

    }
}
