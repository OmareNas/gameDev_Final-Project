using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 1;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Health>() != null)
        {
            Health health = other.GetComponent<Health>();
            health.Damage(damage);
        }
    }
}
