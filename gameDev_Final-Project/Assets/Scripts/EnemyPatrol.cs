using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour  
{
   [SerializeField] private GameObject[] waypoints;
    private int currentWaypointInt =0, nextWayPointInt =0;
    private bool draw = false, isMoving = true;

    public Rigidbody2D rb;
    public Animator anim;

    [SerializeField] private float speed = 2f;//2f;
    
    [SerializeField] private int damage = 1;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if(waypoints.Length != 0)
        {
            //Debug.Log("Ran!");
            currentWaypointInt = 0;
            nextWayPointInt = currentWaypointInt++;
            draw = true;
        }
        anim.SetBool("isMoving", true);
    }

    // Update is called once per frame
    private void  Update()
    {

        if (Vector2.Distance(waypoints[currentWaypointInt].transform.position, transform.position) < 0.1f)
        {
            StartCoroutine(MovingDelay());
            currentWaypointInt= nextWayPointInt;
            nextWayPointInt++;
            if(nextWayPointInt >= waypoints.Length)
            {
                //Debug.Log("Inside next"+nextWayPointInt);
                nextWayPointInt = 0;
                
            }   
        }
        
        if(isMoving)
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointInt].transform.position, Time.deltaTime * speed);
        
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    IEnumerator MovingDelay()
    {
        GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(.7f,1.3f);
        GetComponent<AudioSource>().Play();
        anim.SetBool("isMoving",false);
        isMoving = false;
        yield return new WaitForSeconds(1.5f);
        flip();
        yield return null;
        anim.SetBool("isMoving",true);
        isMoving = true;
    }

   void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<HeroKnight>().Damage(damage);
            //Destroy(gameObject);
        }   
    }

    private void OnDrawGizmos() 
    {
        if(draw)
        {
            Gizmos.DrawWireSphere(waypoints[currentWaypointInt].transform.position, 0.1f);
            Gizmos.DrawWireSphere(waypoints[nextWayPointInt].transform.position, 0.1f);
            Gizmos.DrawLine(waypoints[currentWaypointInt].transform.position, waypoints[nextWayPointInt].transform.position);
        }

    }
}
