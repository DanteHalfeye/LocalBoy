using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    // Only one state at a time
    //Remember that the base state is an abstract class and the states are inheriting from it
    PlayerBaseState currentState;
    PlayerInput input;
    PlayerActor actor;

    [SerializeField] 
    string currentStateName = "";

    Movement playerMovement;


    bool grabbing;
    /*
    
    Place here all your variables, these variables are the ones the states need to funtion correctly
    create bool variables to know what action is inputed in order to change state

    */
    [HideInInspector]
    public ExampleState exampleState;
    [HideInInspector]
    public UnequipedState unequipedState;

    /* Declare the instance of the states - for example

    public PlayerMovingState movingState;
    public PlayerRunningState runningState;
    public PlayerTiredState tiredState;
    public PlayerInteractingState interactingState;
    public PlayerHidingState crouchState;

    */



    /* Getter and setters

    Place here all the variables getters

    for example

    if your variable is PlayerBaseState currentState;

    DON't change it to public, that's a bad practice and can lead to problems
    instead create a PUBLIC variable as a getter/setter

    public PlayerBaseState CurrentState { get { return currentState; } set { currentState = value; } }

    */
    public bool Grabbing { get { return grabbing; } }
    public Movement PlayerMovement { get { return playerMovement; } }
    public PlayerBaseState CurrentState { get { return currentState; } set { currentState = value; } }

    private void Awake()
    {
        playerMovement = GetComponent<Movement>();
        actor = PlayerActor.instance;
        input = GetComponent<PlayerInput>();
        // Set your components here

        /*
         Also set your states with the sintax their constructors need

        example:

            movingState = new PlayerMovingState(this, _playerController, PlayerCamera);
            runningState = new PlayerRunningState(this, _playerController, PlayerCamera);
            tiredState = new PlayerTiredState(this, _playerController, PlayerCamera);
            interactingState = new PlayerInteractingState(this, _playerController, PlayerCamera);
            crouchState = new PlayerHidingState(this, _playerController, PlayerCamera);

         */




        unequipedState = new UnequipedState(this, playerMovement, actor);
    }

    private void Start()
    {
        currentState = unequipedState;
        CurrentState.EnterState();
    }

    void Update()
    {
        currentStateName = currentState.ToString();
        currentState.UpdateState();

    }

    /// <summary>
    /// This method is called by the states, not by the state machine
    /// </summary>
    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        state.EnterState();
    }

    public bool OnGrabInput(InputValue input)
    {
        return actor.OnHoldPress();
    }

}
