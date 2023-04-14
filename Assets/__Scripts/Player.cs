using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float moveSpeed = 0.25f;
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;
    

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();

    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        //reset moveDelta
        moveDelta = new Vector3(x,y,0);

        //swap sprite directions
        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1,1,1);

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0,moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor","Wall"));
        if( hit.collider == null)
        {
            //making movement
            transform.Translate(0,moveDelta.y * Time.deltaTime* moveSpeed, 0);
        }
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor","Wall"));
        if( hit.collider == null)
        {
            //making movement
            transform.Translate(moveDelta.x * Time.deltaTime* moveSpeed,0, 0);
        }
        

    }

}
 