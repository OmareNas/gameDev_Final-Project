using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveExit : MonoBehaviour
{

    [SerializeField] private string sceneStr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
//            other.GetComponent<ObjSpawner>().destroy();
            StartCoroutine(delaySwitchScene());
            SceneManager.LoadScene(sceneStr);            
        }
    }

    IEnumerator delaySwitchScene()
    {
        GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(.7f,1.3f);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(.5f);
        
    }
}
