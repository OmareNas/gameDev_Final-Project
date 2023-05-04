using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SticykPlatform : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player")
        {
            other.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag=="Player")
        {
            other.gameObject.transform.SetParent(null);
        }
    
    }
}
