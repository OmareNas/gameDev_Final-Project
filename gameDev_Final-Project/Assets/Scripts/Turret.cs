using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private GameObject player;

    bool isFlip = false;

    [SerializeField] private float trackingDistance = 8;

    private float timer;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        isFlip = false;
    }

    // Update is called once per frame
    void Update()
    {
//        Debug.Log("turret position: " + transform.position.x + " < playerPos:  " + player.transform.position.x);
        
        float distance = Vector2.Distance(transform.position, player.transform.position);
        //Debug.Log(distance);
       
        if (distance < trackingDistance)
        {
            timer += Time.deltaTime;

            if (timer > 2)
            {
                timer=0;
                shoot();
            }
        }

        if (transform.position.x > player.transform.position.x)
        {
            if(isFlip)
            {
                flip();
                isFlip = false;
            }
        }
        else if (transform.position.x < player.transform.position.x)
        {
            if(!isFlip)
            {
                flip();
                isFlip = true;
            }
        }
        

        
    }

    private void flip()
    {
        //change turret position
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        //change bullet position
        Vector3 bulletPosLocalScale = bulletPos.transform.localScale;
        bulletPosLocalScale.x *= -1;
        bulletPos.transform.localScale = bulletPosLocalScale;
        //rotate the bullet
        // Vector3 bulletLocalScale = bullet.gameObject.transform.localScale;
        // bulletLocalScale.x *= -1;
        // bullet.transform.localScale = bulletLocalScale;
    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
