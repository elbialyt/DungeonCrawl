using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform lookAt;
    public float boundX = 0.15f;
    public float boundY = .05f;

    public void Start()
    {
        lookAt = GameObject.Find("player1").transform;
       
        
      
    }
    private void LateUpdate()
    {
        Vector3 delta = new Vector3(0,0,0);

        //check bounds on x axis
        float deltaX = lookAt.position.x - transform.position.x; ;
        if(deltaX > boundX || deltaX < -boundX)
        {
            if(transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = deltaX + boundX;
            }

        }

        //check bounds on y axis

        float deltaY = lookAt.position.y - transform.position.y; ;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }

        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }

}
