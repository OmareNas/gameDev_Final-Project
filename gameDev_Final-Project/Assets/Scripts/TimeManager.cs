using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [SerializeField] Text pause;
    // Start is called before the first frame update
    void Start()
    {
        ResetTime();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetTime()
    {
        Time.timeScale = 1;
        pause.enabled = false;
        //Time.fixedDeltaTime= .2f;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pause.enabled = true;
        //Time.fixedDeltaTime = .02f * 0;
    }

}
