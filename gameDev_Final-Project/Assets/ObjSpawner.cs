using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSpawner : MonoBehaviour
{
    [Header("Waypoints")]
    //[SerializeField] private GameObject botomLeft , botomRight , topLeft , topRight;
    [SerializeField] private GameObject waypoint1, waypoint2;

    [Header("Obj To Spawn")]
     [SerializeField] private GameObject obj;
    void Start()
    {
        GenerateObj();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateObj()
    {
        StartCoroutine(GeneratePelletsRoutine());
    
        IEnumerator GeneratePelletsRoutine(){
            Debug.Log("GENERATION START!");
            while(true){ //goes forever
                Vector2 randomPosition = new Vector2(Random.Range(waypoint1.transform.position.x,waypoint2.transform.position.x),Random.Range(waypoint1.transform.position.y,waypoint2.transform.position.y)); //random position
                yield return new WaitForSeconds(2f);
                //GameObject newPellet = Instantiate(foodPelletPrefabs[Random.Range(0,foodPelletPrefabs.Count)],randomPosition,Quaternion.identity);
                obj = Instantiate(obj,randomPosition,Quaternion.identity);
                Destroy(obj,10);
            }

        }
    }
    public void destroy()
    {
        Destroy(this.gameObject);
    }
}
