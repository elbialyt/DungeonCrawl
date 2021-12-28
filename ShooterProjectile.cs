using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterProjectile : MonoBehaviour
{
  public float speed;
  private Transform player;
  private Vector2 target;

  void Start(){
      player= GameObject.Find("player1").transform;
      target = new Vector2(player.position.x,player.position.y);
  }
  void Update(){
      transform.position = Vector2.MoveTowards(transform.position,target, speed*Time.deltaTime);
         if(transform.position.x == target.x && transform.position.y==target.y){
          DestroyProjectile();
      }
  }
  
  void DestroyProjectile(){
      Destroy(gameObject);
  }
  
}
