using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class SceneLoader : MonoBehaviour
{
    private PlayerInputHandler input_handler1, input_handler2;
    GameObject player_1, player_2;

    private void Start()
    {
        if(GameObject.FindGameObjectWithTag("Player_1_Input") != null && GameObject.FindGameObjectWithTag("Player_2_Input"))
        {
            input_handler1 = GameObject.FindGameObjectWithTag("Player_1_Input").GetComponent<PlayerInputHandler>();
            input_handler2 = GameObject.FindGameObjectWithTag("Player_2_Input").GetComponent<PlayerInputHandler>();
        }
    }


    public void load_player_1_selection()
    {
        SceneManager.LoadScene("level1");
        SceneManager.LoadScene("Player_1_Character_Selection", LoadSceneMode.Additive);
    }

    public void load_player_2_selection()
    {
        if (input_handler1.player_prefab != null)
        {
            SceneManager.UnloadSceneAsync("Player_1_Character_Selection");
            SceneManager.LoadScene("Player_2_Character_Selection", LoadSceneMode.Additive);
        }

    }

    public void load_fight_game()
    {
        if (input_handler2.player_prefab != null)
        SceneManager.UnloadSceneAsync("Player_2_Character_Selection");
    }

    public void player_1_died()
    {
        SceneManager.LoadScene("Player_1_died");
    }

    public void player_2_died()
    {
        SceneManager.LoadScene("Player_2_died");
    }

    public void load_main_menu()
    {
        SceneManager.LoadScene("Title_screen");
    }
}

