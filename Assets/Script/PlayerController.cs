using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerController : MonoBehaviour
{   
    
    [SerializeField] private GameObject playerGraphic;
    private SwipeDirection swipeDirection = SwipeDirection.none;
    private Vector2 mouseDown;
    private Vector2 mouseUp;
    private RaycastHit hitWall;
    [SerializeField]
    private LayerMask WalllayerMask;
    [SerializeField]
    private Animator animator;
    private Vector3 direction;
    private bool isMoving = false;
    public BrickList listBrick;
    private int score = 0;
    private bool isUp;
    [SerializeField]
    private float movingSpeed;
    private bool isLose;
    
    //private string currentAnimName;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        
        if (isLose)
        {
            return;
        } 

        CheckDirectionToMove();     
        GetVectorDirection(swipeDirection);
        Vector3 targetPos = GetPositiontoMove();
        if (targetPos != transform.position)
        {
           MoveToTarget(targetPos);               
        } 
        if (isUp)
        {
            Invoke(nameof(ResetAnim), 0.5f);
        }
        
    }
    
    private void MoveToTarget(Vector3 targetPos)
    {
        isMoving = true;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, movingSpeed*Time.deltaTime);
        float distance = Vector3.Distance(transform.position, targetPos);
        if (distance < 0.1f)
        {
            isMoving = false;
        }
    }



    //Check 2 points position to get the direction to move
    private void CheckDirectionToMove() {
        if (isMoving)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            
            mouseDown = new Vector2(Input.mousePosition.x, Input.mousePosition.y);           
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseUp = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 checkingVector = mouseUp - mouseDown;
            checkingVector.Normalize();
            if (checkingVector.y > 0 && checkingVector.x < 0.5f && checkingVector.x > -0.5f)
            {
                swipeDirection = SwipeDirection.Forward;
            }
            if (checkingVector.y < 0 && checkingVector.x < 0.5f && checkingVector.x > -0.5f)
            {
                swipeDirection = SwipeDirection.Backward;
            }
            if (checkingVector.x > 0 && checkingVector.y < 0.5f && checkingVector.y > -0.5f)
            {
                swipeDirection = SwipeDirection.Right;
            }
            if (checkingVector.x < 0 && checkingVector.y < 0.5f && checkingVector.y > -0.5f)
            {
                swipeDirection = SwipeDirection.Left;
            }
        }
        
        
       
        
        
    }
    private void GetVectorDirection(SwipeDirection swipeDirection)
    {
        if (swipeDirection == SwipeDirection.Forward)
        {
            direction = Vector3.forward;
        }
        else if (swipeDirection == SwipeDirection.Backward)
        {
            direction = Vector3.back;
        }
        else if (swipeDirection == SwipeDirection.Left)
        {
            direction = Vector3.left;
        }
        else if (swipeDirection == SwipeDirection.Right)
        {
            direction = Vector3.right;
        }
        else
        {
            direction = Vector3.zero;
        }
    }
    private Vector3 GetPositiontoMove()
    {
        
        if(Physics.Raycast(transform.position, direction, out hitWall, Mathf.Infinity, WalllayerMask))
        {
            Vector3 position = hitWall.transform.position;
            position.y = transform.position.y;           
            return position - direction*1f;
        }
        return transform.position;
    }
    public Vector3 GetCurrentPlayerPosition()
    {
        return playerGraphic.transform.position;
    }
    public void SetPlayerPosition(Vector3 position)
    {
        playerGraphic.transform.position = position;
    }
    public void Up()
    {   
        isUp = true;
        animator.Play("Take 2");
    }
    public void Winning()
    {
        animator.Play("Take 3");
    }
    public void ResetAnim()
    {   
        isUp = false;
        animator.Play("New State");
    }

    public void onInit()
    {   
        isLose = false;
        movingSpeed = 15f;
        direction = Vector3.zero;
        swipeDirection = SwipeDirection.none;
        isMoving = false;
        ClearBrickList();
        ResetAnim();
    }
    public void SetScore(int score)
    {
        this.score = score;
    }
    public int GetScore()
    {
        return this.score;
    }
    public void ClearBrickList()
    {  
       int amount = listBrick.brickList.Count;
       for (int i = 0; i < amount; i++)
        {
            listBrick.removeBrick();
        }
    }
    public void Lose()
    {
        isLose = true; 
        UIManager.Instance.EnanbleLoseUI();

    }

    public void SetMovingSpeed(float movingSpeed)
    {
        this.movingSpeed = movingSpeed;
    }
}
