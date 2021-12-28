using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected private BoxCollider2D boxCollider;
    protected private Vector3 moveDelta;
    protected private RaycastHit2D hit;
    public float ySpeed = 0.75f;
    public float xSpeed = 1.0f;
    private Vector3 originalSize;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        originalSize = transform.localScale;
    }
   
    protected virtual void UpdateMotor(Vector3 input)
    {
        //Reset moveDelta
        moveDelta = new Vector3 (input.x *xSpeed, input.y * ySpeed, 0) ;

        //swap sprite direction depending on right or left
        if (moveDelta.x > 0)
            transform.localScale = originalSize;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(originalSize.x * -1, originalSize.y, originalSize.z);

        //pushforce
        moveDelta += pushDirection;

        //Reduce push force
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);


        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //make it move
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //make it move
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }

}
