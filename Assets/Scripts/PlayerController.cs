using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour, IDataPersistence
{
    public GameObject itemCounter;
    public float moveSpeed;
    private Vector2 input;
    private bool isMoving;
    private Animator animator;
    public Inventory playerInventory;

    public LayerMask solidObjectsLayer;
    public LayerMask interactablesLayer;
    public LayerMask animalLayer;
    public LayerMask grownCollision;

    public Quest quest;

    AudioManager audioManager;

    //True = right, false = left
    private bool directionFacing = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        // if(quest.isActive )
        // {
        //     cropuiPanel.SetActive(true);
        // }

        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //this.quest = quest;
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
            audioManager.playSFX(audioManager.harvest);
            Harvest();
            animator.SetTrigger("isHarvesting");
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            animator.ResetTrigger("isHarvesting");
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = 6;
            animator.SetBool("isSprinting", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 3;
            animator.SetBool("isSprinting", false);
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
                itemCounter.SetActive(true); ;
                Destroy(collision.gameObject);

                //Loads pumpkin into inventory.
                Item item = Resources.Load<Item>("ItemData/Pumpkin");
                playerInventory.AddItem(item, 1);

                ItemCounter.instance.increasequestCount();
                quest.goal.Harvested();
                if (quest.goal.IsReached())
                {
                    quest.goal.isCompleted = true;
                    //give player the quest amount they were gathering
                    quest.Complete();
                    ItemCounter.instance.questharvestCounter = 0;
                    quest.questgiver.hideObjective();
                    quest.questgiver.QuestCompletePopup();
                    ItemCounter.instance.normalharvestCounter += quest.goal.requiredAmount;
                    //increase gold
                    Rewards.instance.gold += quest.goldReward;
                    Rewards.instance.increaseGold();
                    //increase xp
                    ExpController.instance.currentExp += quest.expReward;
                    //reset quest amount required to 0 at quest completion, this is so we can replay the quest again 
                }
            }
            else
            {
                Destroy(collision.gameObject);
                
                //Loads pumpkin into inventory.
                Item item = Resources.Load<Item>("ItemData/Pumpkin");
                playerInventory.AddItem(item, 1);
                
                ItemCounter.instance.increasenormalCount();
            }
        }
    }

    public void updateInventory()
    {
        // cash += 1;
        // Debug.Log("Cash generated");
        // cashText.text = "" + cash;
        // itemCounter += 1;1
        // counterText.text = "" + itemCounter;
    }

    public void LoadData(GameData data)
    {
        this.itemCounter.SetActive(data.itemCounter);
        this.transform.position = data.playerPosition;
    }

    public void SaveData(ref GameData data)
    {
        data.itemCounter = this.itemCounter;
        data.playerPosition = this.transform.position;
    }
}
