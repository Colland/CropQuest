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

        if(isWalkable(targetPos))
        {
            position.x = targetPos.x;
            position.y = targetPos.y;
            transform.position = position;
        }
        else
        {
            
        }

        animator.SetBool("isMoving", isMoving);
    }

    private bool isWalkable(Vector2 targetPos)
    {
        Vector2 position = transform.position;

        if(Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null)
        {
            // Check if the collision is happening on the x-axis or y-axis
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                // Try moving only on the y-axis
                targetPos.x = position.x;
            }
            else
            {
                // Try moving only on the x-axis
                targetPos.y = position.y;
            }

            // Check if the new target position is walkable
            if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) == null)
            {
                return true;
            }
            
            return false;
        }
        
        return true;
    }
}
