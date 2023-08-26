using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 input;
    private bool isMoving;
    private Animator animator;

    public LayerMask solidObjectsLayer;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if(!(input.x == 0 && input.y == 0))
        {
            isMoving = true;
            animator.SetFloat("moveX", input.x);
            animator.SetFloat("moveY", input.y);
        }
        else
        {
            isMoving = false;
        }

        Vector2 position = transform.position;
        Vector2 targetPos;
        targetPos.x = position.x + moveSpeed * input.x * Time.deltaTime;
        targetPos.y = position.y + moveSpeed * input.y * Time.deltaTime;

        if(isWalkable(ref targetPos))
        {
            position.x = targetPos.x;
            Debug.Log(targetPos.y);
            position.y = targetPos.y;
            transform.position = position;
        }

        animator.SetBool("isMoving", isMoving);
    }

    private bool isWalkable(ref Vector2 targetPos)
    {
        Vector2 position = transform.position;
        Collider2D collider = Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer);

        //Checks if the player is colliding with something.
        if(collider != null)
        {   
            Vector3 newPos = transform.position;
            newPos.y = newPos.y+0.1f;

            //Checks if collision is north of player, cancels vertical player movement if so.
            if(Physics2D.OverlapCircle(newPos, 0.2f, solidObjectsLayer) != null)
            {
                Debug.DrawLine(transform.position, newPos, Color.red, 1f);
                targetPos.y = transform.position.y;
            }

            newPos = transform.position;
            newPos.y = newPos.y-0.1f;

            //Checks if collision is south of player, cancels vertical player movement if so.
            if(Physics2D.OverlapCircle(newPos, 0.2f, solidObjectsLayer) != null)
            {
                Debug.DrawLine(transform.position, newPos, Color.red, 1f);
                targetPos.y = transform.position.y;
            }

            newPos = transform.position;
            newPos.x = newPos.x+0.1f;
            
            //Checks if collision is east of player, cancels horizontal player movement if so.
            if(Physics2D.OverlapCircle(newPos, 0.2f, solidObjectsLayer) != null)
            {
                Debug.DrawLine(transform.position, newPos, Color.red, 1f);
                targetPos.x = transform.position.x;
            }

            newPos = transform.position;
            newPos.x = newPos.x-0.1f;
            
            //Checks if collision is west of player, cancels horizontal player movement if so.
            if(Physics2D.OverlapCircle(newPos, 0.2f, solidObjectsLayer) != null)
            {
                Debug.DrawLine(transform.position, newPos, Color.red, 1f);
                targetPos.x = transform.position.x;
            }

            return true;
        }
        
        return true;
    }
}
