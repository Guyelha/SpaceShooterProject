using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInput playerInput;
    private Shooter shooter;

    
    [SerializeField] private float movementSpeed = 4f;

    private Vector2 movementInputVector;

    private Vector2 minScreenBounds;
    private Vector2 maxScreenBounds;
    
    [SerializeField] private float paddingTop, paddingBottom, paddingLeft, paddingRight;

    private void Awake()
    {
        //playerInput = GetComponent<PlayerInput>();

        //playerInput.onActionTriggered += HandleActionTriggered;

        playerInput = new PlayerInput();
        playerInput.Player.Enable();

        shooter = GetComponent<Shooter>();

        playerInput.Player.Fire.performed += HandleFire;
        playerInput.Player.Fire.canceled += HandleFire;
    }

    private void OnDestroy()
    {
        //playerInput.onActionTriggered -= HandleActionTriggered;

        playerInput.Player.Fire.performed -= HandleFire;
        playerInput.Player.Fire.canceled -= HandleFire;
    }

    private void Start()
    {
        InitBounds();
    }

    private void InitBounds()
    {
        Camera camera = Camera.main;

        minScreenBounds = camera.ViewportToWorldPoint(new Vector2(0, 0));
        maxScreenBounds = camera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private void Update()
    {
        GetMovementInput();
        Move();
    }

    #region INPUT (Send messages)
    /*
    public void OnFire(InputValue value)
    {
        Debug.Log("FIRE " + value);
    }

    public void OnMove(InputValue value)
    {
        Debug.Log("MOVE " + value.Get<Vector2>());
    }
    */
    #endregion

    #region INPUT (Unity Events)
    /*
    public void FireInput(InputAction.CallbackContext ctx)
    {
        Debug.Log(ctx.phase);

        if (ctx.phase == InputActionPhase.Performed)
        {
            Debug.Log("FIRE");
        }
    }

    public void MoveInput(InputAction.CallbackContext ctx)
    {
        Debug.Log("MOVE " + ctx.phase);
        Debug.Log(ctx.ReadValue<Vector2>());
    }
    */
    #endregion

    #region INPUT (c# event)
    /*
    private void HandleActionTriggered(InputAction.CallbackContext ctx)
    {
        if(ctx.action == playerInput.actions.FindAction("Fire"))
        {
            if (ctx.performed)
            {
                Debug.Log("FIRE");
            }
        }
        else if (ctx.action == playerInput.actions.FindAction("Move"))
        {
            if (!ctx.started)
            {
                Debug.Log(ctx.phase + "  " + ctx.ReadValue<Vector2>());
                movementInputVector = ctx.ReadValue<Vector2>();
                //ChangeVelocity(ctx.ReadValue<Vector2>());

            }

            

        }
    }
    */
    #endregion



    #region MOVEMENT

    private void GetMovementInput()
    {
        movementInputVector = playerInput.Player.Move.ReadValue<Vector2>();
    }
    
    private void Move()
    {
        Vector2 newPosition = new Vector2(
            transform.position.x + movementInputVector.x * movementSpeed * Time.deltaTime,
            transform.position.y + movementInputVector.y * movementSpeed * Time.deltaTime);

        newPosition.x = Mathf.Clamp(newPosition.x, minScreenBounds.x + paddingLeft, maxScreenBounds.x - paddingRight);
        newPosition.y = Mathf.Clamp(newPosition.y, minScreenBounds.y + paddingBottom, maxScreenBounds.y - paddingTop);


        transform.position = newPosition;
    }

    void ChangeVelocity(Vector2 newVelocity)
    {
        //rb.changevelocity(newVelocity);
        //rb.changevelocity(newVelocity);
    }

    #endregion

    #region COMBAT

    private void HandleFire(InputAction.CallbackContext ctx)
    {
        Debug.Log("FIRE: " + ctx.ReadValueAsButton());

        shooter.isShooting = ctx.ReadValueAsButton();
    }

    #endregion


}
