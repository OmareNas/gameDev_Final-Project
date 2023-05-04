using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public bool isOpen;
    public Animator animator;
    
    public GameObject[] ObjectPrefab;

    int objSize = 0;
    
    void Start()
    {
        //isOpen = false;
        animator = GetComponent<Animator>();
        objSize = ObjectPrefab.Length;
    }

    
    void Update()
    {

        
    }

    public void OpenChest()
    {
        if(!isOpen)
        {
            isOpen = true;
            Debug.Log("Chest is now open!");
            StartCoroutine(openChestDelay());
        }
    }

    public void HittenChest()
    {
        
    }

    IEnumerator openChestDelay()
    {
        GetComponent<AudioSource>().pitch = UnityEngine.Random.Range(.7f,1.3f);
        GetComponent<AudioSource>().Play();
        animator.SetBool("Opening", true);
        yield return new WaitForSeconds(.5f);
        animator.SetBool("Opening", false);
        animator.SetBool("isOpen", true);
        randomLoot();

    }

    public void randomLoot()
    {
        float chestPosX = transform.position.x;
        float chestPosY = transform.position.y;
        float dis = .5f; 

        int heartSpawner =  (int)Random.Range(1,3);
        int objSpawner =  (int)Random.Range(1,4);

        Debug.Log("Heart Spawner: "+ heartSpawner);
        Debug.Log("obj Spawner: "+ objSpawner);

        int count=0;
        

        for (int i=0; i < ObjectPrefab.Length; i++)
        {
            Vector2 randomObjPrefabPos = new Vector2(Random.Range(chestPosX-dis,chestPosX+dis),chestPosY);

            if(i==0)
            {
                while (count < heartSpawner)
                {
                    randomObjPrefabPos = new Vector2(Random.Range(chestPosX-dis,chestPosX+dis),chestPosY);
                    Instantiate(ObjectPrefab[0], randomObjPrefabPos, Quaternion.identity);
                    count++;
                }
                    
            }
            else
            {
                for (int j = 0; j < objSpawner; j++)
                    Instantiate(ObjectPrefab[i], randomObjPrefabPos, Quaternion.identity);
            }
        }
        
    }

     
}
