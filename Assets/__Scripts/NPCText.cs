using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCText : Collidable
{
    public string message;
    private float cooldown = 4.0f;
    private float lastCooldown;

    protected override void Start()
    {
        base.Start();
        lastCooldown = -cooldown;
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (Time.time - lastCooldown > cooldown)
        {
            lastCooldown = Time.time;
            GameManager.instance.ShowText(message, 22, Color.white,transform.position + new Vector3(0,0.22f,0), Vector3.zero, cooldown);
        }
    }
}
