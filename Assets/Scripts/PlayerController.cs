using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour, IDataPersistence
{
    public GameObject questcompletePopup;
    public GameObject cropuiPanel;
    public GameObject questcropuiPanel;
    public float moveSpeed;
    private Vector2 input;
    private bool isMoving;
    private Animator animator;

    public LayerMask solidObjectsLayer;
    public LayerMask interactablesLayer;
    public LayerMask animalLayer;
    public LayerMask grownCollision;

    public int itemCounter = 0;
    public int questitemCounter = 0;
    public int reqAmount = 0;
    public int cash = 0;
    public TMP_Text counterText;

    public TMP_Text cashText;

    public TMP_Text goal;

    public TMP_Text questcountText;

    public Quest quest;

    //True = right, false = left
    private bool directionFacing = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        cropuiPanel.SetActive(true);
        cashText.text = "" + cash;
        reqAmount = quest.goal.requiredAmount;

        goal.text = "" + reqAmount;
        counterText.text = "" + itemCounter;

        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");


        if (input.x == 0 && !(input.y == 0))
        {
            isMoving = true;
            animator.SetFloat("moveY", input.y);
        }

        else if (!(input.x == 0 && input.y == 0))
        {
            isMoving = true;
            animator.SetFloat("moveX", input.x);
            animator.SetFloat("moveY", input.y);

            if (input.x > 0)
            {
                directionFacing = true;
            }
            else
            {
                directionFacing = false;
            }
        }
        else
        {
            isMoving = false;

            if (input.x > 0)
            {
                directionFacing = true;
            }
            else
            {
                directionFacing = false;
            }
        }

        Vector2 position = transform.position;
        Vector2 targetPos;
        targetPos.x = position.x + moveSpeed * input.x * Time.deltaTime;
        targetPos.y = position.y + moveSpeed * input.y * Time.deltaTime;

        if (isWalkable(ref targetPos))
        {
            position.x = targetPos.x;
            position.y = targetPos.y;
            transform.position = position;
        }

        animator.SetBool("isMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Harvest();
            animator.SetTrigger("isHarvesting");
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            animator.ResetTrigger("isHarvesting");
        }
    }

    public void ResetHarvestTrigger()
    {
        animator.ResetTrigger("isHarvesting");
        animator.SetTrigger("idle");
    }

    private bool isWalkable(ref Vector2 targetPos)
    {
        Vector2 position = transform.position;
        Collider2D collider = Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer | interactablesLayer | animalLayer);

        //Checks if the player is colliding with something.
        if (collider != null)
        {
            Vector3 newPos = transform.position;
            newPos.y = newPos.y + 0.1f;

            //Checks if collision is north of player, cancels vertical player movement if so.
            if (Physics2D.OverlapCircle(newPos, 0.1f, solidObjectsLayer | interactablesLayer | animalLayer) != null)
            {
                if (targetPos.y > transform.position.y)
                {
                    targetPos.y = transform.position.y;
                }
                Debug.Log("North");
            }

            newPos = transform.position;
            newPos.y = newPos.y - 0.1f;

            //Checks if collision is south of player, cancels vertical player movement if so.
            if (Physics2D.OverlapCircle(newPos, 0.1f, solidObjectsLayer | interactablesLayer | animalLayer) != null)
            {
                if (targetPos.y < transform.position.y)
                {
                    targetPos.y = transform.position.y;
                }
                Debug.Log("South");
            }

            newPos = transform.position;
            newPos.x = newPos.x + 0.1f;

            //Checks if collision is east of player, cancels horizontal player movement if so.
            if (Physics2D.OverlapCircle(newPos, 0.1f, solidObjectsLayer | interactablesLayer | animalLayer) != null)
            {
                if (targetPos.x > transform.position.x)
                {
                    targetPos.x = transform.position.x;
                }
            }

            newPos = transform.position;
            newPos.x = newPos.x - 0.1f;

            //Checks if collision is west of player, cancels horizontal player movement if so.
            if (Physics2D.OverlapCircle(newPos, 0.1f, solidObjectsLayer | interactablesLayer | animalLayer) != null)
            {
                if (targetPos.x < transform.position.x)
                {
                    targetPos.x = transform.position.x;
                }
            }

            return true;
        }

        return true;
    }

    public void Interact()
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;

        Debug.DrawLine(transform.position, interactPos, Color.red, 5f);

        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactablesLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }


    }
    private void Harvest()
    {

        float directionFloat = 0;

        if (directionFacing == true)
        {
            directionFloat = 1;
        }
        else
        {
            directionFloat = -1;
        }

        var interactPos = transform.position;
        interactPos.x += directionFloat;

        Collider2D collision = Physics2D.OverlapCircle(interactPos, 0.5f, grownCollision);

        if (collision != null)
        {
            if (quest.isActive)
            {
                Destroy(collision.gameObject);
                questitemCounter++;
                questcountText.text = "" + questitemCounter;
                quest.goal.Harvested();

                if (quest.goal.IsReached())
                {
                    cash += quest.goldReward;
                    questitemCounter = 0;
                    questcompletePopup.SetActive(true);
                    questcropuiPanel.SetActive(false);
                    cropuiPanel.SetActive(true);
                    quest.Complete();
                    quest.goal.Reset();
                }
            }
            else
            {
                Destroy(collision.gameObject);

                Inventory inventory = FindObjectOfType<Inventory>();
                inventory.addToInv(collision.gameObject);

                itemCounter++;
                counterText.text = "" + itemCounter;
            }
        }
    }

    public void updateInventory()
    {
        // cash += 1;
        // Debug.Log("Cash generated");
        // cashText.text = "" + cash;
        // itemCounter += 1;
        // counterText.text = "" + itemCounter;
    }

    public void LoadData(GameData data)
    {
        this.cash = data.cash;
        this.itemCounter = data.itemCounter;
        this.transform.position = data.playerPosition;
        this.questitemCounter = data.questitemCounter;
        this.quest.isActive = data.isQuestActive;
    }

    public void SaveData(ref GameData data)
    {
        data.cash = this.cash;
        data.itemCounter = this.itemCounter;
        data.playerPosition = this.transform.position;
        data.questitemCounter = this.questitemCounter;
        data.isQuestActive = quest.isActive;
    }
}
