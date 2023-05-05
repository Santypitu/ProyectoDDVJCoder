using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int health = 100;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyWeapon")
        {
            Debug.Log("Colisiono el arma con el jugador");
            health -= 10;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Die");
    }
}
