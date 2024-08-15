using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    // Only one state at a time
    //Remember that the base state is an abstract class and the states are inheriting from it
    EnemyBaseState currentState;

    [ShowOnly]
    [SerializeField] 
    string currentStateName = "";

    /*
    
    Place here all your variables, these variables are the ones the states need to funtion correctly
    create bool variables to know what action is inputed in order to change state

    */
    [HideInInspector]
    public EnemyExampleState exampleState;
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
    public EnemyBaseState CurrentState { get { return currentState; } set { currentState = value; } }

    private void Awake()
    {
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
        exampleState = new EnemyExampleState(this);
    }

    private void Start()
    {
        currentState = exampleState;
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
    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState();
    }


}
