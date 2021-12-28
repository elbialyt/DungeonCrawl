using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private float timeBtwn;
    public float start;
    public float chase;
   

    public GameObject projectile;

    void Start(){
        timeBtwn = start;
    }

   void Update(){
       if(timeBtwn<=0 && Vector3.Distance(GameManager.instance.player.transform.position,transform.position)<chase){
           Instantiate(projectile, transform.position, Quaternion.identity);
           timeBtwn = start;

       }
       else{
           timeBtwn -=Time.deltaTime;
       }
   }
}
