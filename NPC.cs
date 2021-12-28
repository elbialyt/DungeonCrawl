using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Collidable
{
    public string message;
    private float cooldown = .001f;
    private float lastTime;

    protected override void Start()
    {
        base.Start();
        lastTime = -cooldown;
    }


    protected override void OnCollide(Collider2D coll)
    {
        if (Time.time - lastTime > cooldown)
        {
            lastTime = Time.time;
            GameManager.instance.ShowText(message, 20, Color.white, transform.position + new Vector3(0,0.20f,0), Vector3.zero, cooldown);

        }
    }
}
