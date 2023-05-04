using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool isInRange;
    public KeyCode key;
    public UnityEvent interactAction;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange) // if in range to interact
        {
            if(Input.GetKeyDown(key)) //player press key
            {
                interactAction.Invoke(); 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            isInRange = true;
            Debug.Log("Player now in Range");
        }   
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Player")
        {
            isInRange = false;
            Debug.Log("Player not in Range");
        }  
    }

}
