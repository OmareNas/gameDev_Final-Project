using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            //if(other.GetComponent<HeroKnight>().getCurrentHealth() != other.GetComponent<HeroKnight>().getMaxHealth())
            if(true)
            {
                other.GetComponent<HeroKnight>().addPoint();
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
