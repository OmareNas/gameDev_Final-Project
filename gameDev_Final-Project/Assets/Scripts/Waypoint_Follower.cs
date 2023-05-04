using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint_Follower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointInt =0;
    private int nextWayPointInt =0;
    private bool draw = false;
    
    // [SerializeField] private Script script;
    // [SerializeField] private bool scriptObjEnable =false;
    [SerializeField] private float speed = 2f;//2f;

    // Start is called before the first frame update
    private void Start()
    {
        if(waypoints.Length != 0)
        {
            //Debug.Log("Ran!");
            currentWaypointInt = 0;
            nextWayPointInt = currentWaypointInt++;
            draw = true;
        }
    }

    // Update is called once per frame
    private void  Update()
    {
        //nextWayPointInt =0;
        //nextWayPointInt = 0;
        if (Vector2.Distance(waypoints[currentWaypointInt].transform.position, transform.position) < 0.1f)
        {
            
            currentWaypointInt= nextWayPointInt;
            nextWayPointInt++;
            if(nextWayPointInt >= waypoints.Length)//waypoints.Length)
            {
                //Debug.Log("Inside next"+nextWayPointInt);
                nextWayPointInt = 0;
            }

            // if (currentWaypointInt >= waypoints.Length)
            // {Debug.Log("Inside current"+currentWaypointInt);
            //     currentWaypointInt = 0;
                
            // }
            
            
            
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointInt].transform.position, Time.deltaTime * speed);
        
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
