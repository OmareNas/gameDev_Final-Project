using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heart : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            if(other.GetComponent<HeroKnight>().getCurrentHealth() != other.GetComponent<HeroKnight>().getMaxHealth())
            {
                other.GetComponent<HeroKnight>().Heal(2);

                GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(.7f,1.3f);
                GetComponent<AudioSource>().Play();
                
                transform.position = new Vector3(1000000,0,0);
                Destroy(gameObject,2);
            }
        }
        if(other.gameObject.CompareTag("Spike"))
        {
            Destroy(gameObject);
        }
    }

    
}
