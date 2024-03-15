using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageSpecialAttacks : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Player_2"))
        {
            other.gameObject.GetComponent<PlayerController>().take_damage(3);
        }
    }
}
