using System;
using System.Collections;
using System.Collections.Generic;
using Code.Pickup;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropLoot : MonoBehaviour
{
    [SerializeField] private List<DropItem> dropList = new List<DropItem>();
    [SerializeField] private bool DropOnce;
    // Start is called before the first frame update
    void Start()
    {
        CycleListAndMakeLoot();
        Destroy(gameObject);
    }

    private void CycleListAndMakeLoot()
    {
        foreach (var dropItem in dropList)
        {
            if (DropIfRandomValueBiggerThanMinChance(dropItem) && DropOnce) break;
        }
    }

    private bool DropIfRandomValueBiggerThanMinChance(DropItem item)
    {
        var chance = Random.Range(0f, 100f);
        if (chance <= item.lootChance)
        {
            Instantiate(item.lootObject, transform.position, quaternion.identity);
            return true;
        }

        return false;
    }

}
