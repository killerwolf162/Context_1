using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageNormalAttacks : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player_2"))
        {
            other.gameObject.GetComponent<PlayerController>().take_damage(1);
        }
    }

}
