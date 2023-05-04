using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour
{
    private HeroKnight hero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player")
        {
            Debug.Log("inside func death");
            
            other.GetComponent<HeroKnight>().Die();
            //WaitForSecondsRealtime(2);
           // WaitForEndOfFrame();

           // SceneManager.LoadScene("MainMenu");

        }
    }

    private void WaitForEndOfFrame()
    {
        throw new NotImplementedException();
    }

    private void WaitForSecondsRealtime(int v)
    {
        throw new NotImplementedException();
    }

    public void switchScene(String scene)
    {
        
    }
}
