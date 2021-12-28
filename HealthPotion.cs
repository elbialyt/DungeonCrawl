using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Collidable
{
     


    protected override void OnCollide(Collider2D coll)
    {
        base.OnCollide(coll);
        if (coll.name != "player1")
            return;
       
            Destroy(gameObject);
             GameManager.instance.ShowText("HEALED", 40, Color.red , transform.position, Vector3.up * 30, 1.0f);
           GameManager.instance.player.Heal(GameManager.instance.player.maxHitpoints);
       
    }
    
}
