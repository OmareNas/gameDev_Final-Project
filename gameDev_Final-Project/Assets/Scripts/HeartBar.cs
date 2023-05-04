using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBar : MonoBehaviour
{
    public GameObject heartPrefab;
    public HeroKnight player;
    List<HealthHeart> hearts = new List<HealthHeart>();

    private void OnEnable() {
        HeroKnight.onPlayerDamage += DrawHeart;
        HeroKnight.onPlayerDeath += DrawHeart;
    }

    private void OnDisable() {
        HeroKnight.onPlayerDamage -= DrawHeart;
        HeroKnight.onPlayerDeath -= DrawHeart;

    }

    public void Start()
    {
        DrawHeart();
    }
    
    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HealthHeart heartComp = newHeart.GetComponent<HealthHeart>();
        heartComp.setHeartImage(HeartStatus.Empty);
        hearts.Add(heartComp);
    }

    public void DrawHeart()
    {
        ClearHeart();
        float maxHealthRemainder = player.getMaxHealth() % 2;
        //int heartToMake = (int) ((player.getMaxHealth() / 2) + maxHealthRemainder);
        int heartToMake = (int) ((player.getMaxHealth() / 2) + maxHealthRemainder);

        for(int i =0; i < heartToMake; i++)
        {
            CreateEmptyHeart();
        }

        for(int i =0; i < hearts.Count; i++)
        {
            int heartStatRemainder = (int) Mathf.Clamp(player.getCurrentHealth() - (i*2), 0, 2);
            hearts[i].setHeartImage((HeartStatus)heartStatRemainder);
        }

    }

    public void ClearHeart()
    {
        foreach(Transform trans in transform)
        {
           Destroy(trans.gameObject); 
        }
        hearts = new List<HealthHeart>();
    }

}
