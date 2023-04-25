using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    private SpriteRenderer spriteRenderer;

    
    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        DontDestroyOnLoad(gameObject);
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));
    }
    public void SwapSprite(int skinId)
    {
       spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }

    public void OnLevelUp()
    {
        maxHitpoint++;
        hitpoint = maxHitpoint;
        //Debug.Log("onlevelup" + maxHitpoint);
    }

    public void SetLevel(int level)
    {
        int diffHp = 1;
        for (int i = 1; i < level; i++)
        {
            OnLevelUp();
            diffHp++;
        }
        hitpoint -= diffHp - 1;
        maxHitpoint -= diffHp - 1;
    }

    public void Heal(int healingAmount)
    {
        if (hitpoint == maxHitpoint)
            return;
        hitpoint += healingAmount;
        if (hitpoint > maxHitpoint)
            hitpoint = maxHitpoint;        
        GameManager.instance.ShowText("+" + healingAmount.ToString() + "hp", 25 , Color.green, transform.position, Vector3.up*30, 1.0f );
        
    }
}
 