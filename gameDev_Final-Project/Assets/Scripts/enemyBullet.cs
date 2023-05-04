using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    private float timer;
    public int damage =1;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");

        Vector3 playerPos = player.transform.position;
        playerPos.y += 0.585865f;
        playerPos.x += 0.02457f;
        player.transform.position = player.transform.position;

        //Vector3 direction = player.transform.position - transform.position; y+0.282
        //x += 0.02457, y+=0.585865
        Vector3 direction = playerPos - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rotation = Mathf.Atan2(-direction.y,-direction.x) *Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rotation);

        GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(.7f,1.3f);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 10)
        {
            Destroy(gameObject);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<HeroKnight>().Damage(damage);
            transform.position = new Vector3(1000000,0,0);
            Destroy(this.gameObject,2);
        }
        
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Health>().Damage(damage);
            transform.position = new Vector3(1000000,0,0);
            Destroy(this.gameObject,2);
        }
        
        if(other.gameObject.CompareTag("ground") || other.gameObject.CompareTag("Spike"))
        {
            transform.position = new Vector3(1000000,0,0);
            Destroy(this.gameObject,2);
        }
    }
}
