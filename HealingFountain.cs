using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : Collidable
{
    public int healingAmount = 3;
    private float healCooldown = 1.0f;
    private float lastHeal;

    protected override void OnCollide(Collider2D coll)
    {
        base.OnCollide(coll);
        if (coll.name != "player1")
            return;
        if(Time.time - lastHeal> healCooldown)
        {
            lastHeal = Time.time;
            GameManager.instance.player.Heal(healingAmount);
        }
    }
}
