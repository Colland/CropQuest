using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowBehavior : MonoBehaviour
{
    public float speed;
    public float range;
    public float maxDistance;
    //Left = 0, Right = 1
    public float currentDirection;

    private Animator animator;
    public LayerMask solidObjectsLayer;
    public LayerMask interactablesLayer;


    Vector2 wayPoint;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        setNewDestination();
    }

    void Update()
    {
        Vector2 targetPos = Vector2.MoveTowards(transform.position, wayPoint, speed* Time.deltaTime);
        Collider2D collider = Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer | interactablesLayer);

        if(collider == null)
        {
             if((wayPoint.x - transform.position.x) > 0)
            {
                currentDirection = 1;
            }
            else
            {
                currentDirection = -1;
            }
            animator.SetFloat("Direction", currentDirection);

            transform.position = Vector2.MoveTowards(transform.position, wayPoint, speed* Time.deltaTime);
            animator.SetBool("isMoving", true);

            if(Vector2.Distance(transform.position, wayPoint) < range)
            {
                setNewDestination();
            }
        }
        else
        {
            setNewDestination(); 
        }
    }

    void setNewDestination()
    {
        wayPoint = new Vector2(transform.position.x + Random.Range(-maxDistance, maxDistance),
                               transform.position.y + Random.Range(-maxDistance, maxDistance));
    }
}
