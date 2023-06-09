using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    [Header("Set in Inspector")]
    public int pesosAmount = 10;
    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.pesos += pesosAmount;
            GameManager.instance.ShowText("+"+pesosAmount + " pesos!", 25, Color.yellow, transform.position, Vector3.up*50, 3.0f);
        }
    }
    
}
