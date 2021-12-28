using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : Collidable
{
    public float speedmultiplier = 2.0f;
  private float applied;
  public float cooldown;
    

    protected override void OnCollide(Collider2D coll)
    {
        base.OnCollide(coll);
        if (coll.name != "player1")
            return;
         
            Destroy(gameObject);
            applied = Time.time;
            GameManager.instance.ShowText("SPEED BOOST", 40, Color.blue , transform.position, Vector3.up * 30, 1.0f);

       
        GameManager.instance.player.xSpeed = GameManager.instance.player.xSpeed * speedmultiplier;
        GameManager.instance.player.ySpeed = GameManager.instance.player.ySpeed *  speedmultiplier;
        
          
    }
    
    
       
   
    
}
