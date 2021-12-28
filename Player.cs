using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Mover

{
    
    private SpriteRenderer spriteRenderer;
    private bool isLive = true;

    protected override void ReceiveDamage(Damage dmg)
    {
        if (!isLive)
            return;
        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitpointChange();
    }

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

        
    }
    public void Heal(int healingAmount)
    {
        if(hitpoints == maxHitpoints)
        {
            return;
        }
        hitpoints += healingAmount;
        if (hitpoints > maxHitpoints)
        {
            hitpoints = maxHitpoints;
        }
        
        GameManager.instance.ShowText("+" + healingAmount.ToString() + " hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
        GameManager.instance.OnHitpointChange();


    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if(isLive)
        UpdateMotor(new Vector3(x, y, 0));
    }

    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }

    public void OnLevelUp()
    {
        int b = GameManager.instance.GetCurrentLevel();
        for(int i =0; i < b; i++){
        maxHitpoints++;
        hitpoints = maxHitpoints;
        }
    }
    public void SetLevel(int Level)
    {
        for(int i = 0;i<Level; i++)
        {
            OnLevelUp();
        }
    }

    protected override void Death()
    {
        isLive = false;
        GameManager.instance.deathMenuAnim.SetTrigger("Show");
    }
    public void Respawn()
    {
        Heal(maxHitpoints);
        isLive = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
       
    }
}
