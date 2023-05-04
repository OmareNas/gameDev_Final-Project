using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
   [SerializeField] private int health = 10;

   [SerializeField] private int MAX_HEALTH = 10;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.LeftBracket))
            Damage(1);
        if(Input.GetKeyDown(KeyCode.RightBracket))
            Heal(1);
        if(Input.GetKeyDown(KeyCode.U))
            Debug.Log(getHealth());    

    }

    public void Damage(int amnt)
    {
        if(amnt < 0)
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");

        this.health -=amnt;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(int amnt)
    {
        if(amnt < 0)
            throw new System.ArgumentOutOfRangeException("Cannot have negative Healing");

        bool isOverMaxHealth = health + amnt > MAX_HEALTH;    

        if(isOverMaxHealth)
            this.health = MAX_HEALTH;
        else
         this.health += amnt;
    }

    public int getHealth()
    {
        int h = this.health;
        return (int)h;
    }

    private void Die()
    {
        Debug.Log("Enemy is Dead!");
        this.transform.position = new Vector3(1000000,0,0);
        Destroy(gameObject,2);
    }
}
