using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionManager : MonoBehaviour
{

    private PlayerInputHandler player_input_handler;
    private PlayerInputHandler player_input_handler_2;

    [SerializeField]
    public GameObject joeri_1, joeri_2, tim_1, tim_2, norbert_1, norbert_2, raf_1, raf_2;


    private void Awake()
    {
        player_input_handler = GameObject.FindGameObjectWithTag("Player_1_Input").GetComponent<PlayerInputHandler>();
        player_input_handler_2 = GameObject.FindGameObjectWithTag("Player_2_Input").GetComponent<PlayerInputHandler>();
    }

    public void select_joeri_1()
    {
        player_input_handler.player_prefab = joeri_1;
    }

    public void select_joeri_2()
    {
        player_input_handler_2.player_prefab = joeri_2;
    }
    public void select_tim_1()
    {
        player_input_handler.player_prefab = tim_1;
    }

    public void select_tim_2()
    {
        player_input_handler_2.player_prefab = tim_2;
    }

    public void select_norbert_1()
    {
        player_input_handler.player_prefab = norbert_1;
    }

    public void select_norbert_2()
    {
        player_input_handler_2.player_prefab = norbert_2;
    }

    public void select_raf_1()
    {
        player_input_handler.player_prefab = raf_1;
    }

    public void select_raf_2()
    {
        player_input_handler_2.player_prefab = raf_2;
    }

    public void spawn_player()
    {
        player_input_handler.spawn_player();
        player_input_handler_2.spawn_player();
    }
}
