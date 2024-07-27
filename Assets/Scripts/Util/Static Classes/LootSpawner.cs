using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LootSpawner 
{
    public static List<Item> InitiateLoot(LootPool pool,int LootAmount)
    {
        List<Item> items = new List<Item>();
        int ItemIndex = 0;

        for (int i = 0; i < LootAmount; i++)
        {
            int rarity = Random.Range(0, (int)(pool.Chances.x+ pool.Chances.y+ pool.Chances.z));

            if(rarity < (int)pool.Chances.x)
            {
                Debug.Log("Common");
                ItemIndex = Random.Range(0, pool.Common.Count);
                items.Add(pool.Common[ItemIndex]);


            }else if (rarity < (int)(pool.Chances.x + pool.Chances.y))
            {
                Debug.Log("Rare");
                ItemIndex = Random.Range(0, pool.Rare.Count);
                items.Add(pool.Rare[ItemIndex]);
            }
            else if (rarity < (int)(pool.Chances.x + pool.Chances.y + pool.Chances.z))
            {
                Debug.Log("Legendary");
                ItemIndex = Random.Range(0, pool.Legendary.Count);
                items.Add(pool.Legendary[ItemIndex]);
            }

        }




        return items;
    }



}

