using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class SceneLoader : MonoBehaviour
{

    public void load_player_1_selection()
    {

        SceneManager.LoadScene("level1");
        SceneManager.LoadScene("Player_1_Character_Selection", LoadSceneMode.Additive);
    }

    public void load_player_2_selection()
    {
        SceneManager.UnloadSceneAsync("Player_1_Character_Selection");
        SceneManager.LoadScene("Player_2_Character_Selection", LoadSceneMode.Additive);
    }

    public void load_fight_game()
    {
        SceneManager.UnloadSceneAsync("Player_2_Character_Selection");
    }

}
