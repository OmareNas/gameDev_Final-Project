using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokenGrass : MonoBehaviour
{
    //[SerializeField] private GameObject spawnPos;

    Vector3 spwnPos;
    Rigidbody2D rb;

    void Start()
    {
        spwnPos = transform.position;
        //rb = GetComponent<Rigidbody2D>();
    }  

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Spike" || other.tag =="ground" || other.tag =="Enemy")
        {
            transform.position = new Vector3(1000000,0,0);
            Destroy(this.gameObject, 2);
            //StartCoroutine(respawnDelay());
            // Destroy(gameObject);
        }
        
    }

    // IEnumerator respawnDelay()
    // {
    //     // Debug.Log("Hello");
    //     transform.position = new Vector3(1000000,0,0);
    //     yield return new WaitForSeconds(2f);
    //     //transform.position = spwnPos;

    //     //maybe move grass class and destroy then instiate a new one like enemybullet
    //     //add if time allows
    //     Destroy(this.gameObject, 2);
    // }
}
