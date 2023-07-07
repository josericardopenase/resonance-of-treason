using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public string currentStateName;
    private Dictionary<string, AbstractState> stateTable;
    PlayerMotor motor;


    private void Start()
    {
        stateTable = new Dictionary<string, AbstractState>();
        motor = GetComponent<PlayerMotor>();
        PlayerController controller = this.GetComponent<PlayerController>();
        AddState(new MoveState(motor, controller));
        AddState(new JumpState(motor, controller));
        AddState(new HookState(motor, controller));
        AddState(new AirState(motor, controller));
        AddState(new WallGripState(motor, controller));
        AddState(new DashState(motor, controller));
        AddState(new RollingState(motor, controller));
        ChangeState("MoveState");
    }



    private void Update()
    {
        stateTable[currentStateName].Transition();
	    stateTable[currentStateName].Update();
    }

    private void FixedUpdate()
    {
        stateTable[currentStateName].FixedUpdate();
    }

    public void AddState(AbstractState newState)
    {
        if (!stateTable.ContainsKey(newState.name))
        {
            stateTable.Add(newState.name, newState);
        }
        else
        {
            Debug.LogWarning("State " + newState.name + " already exists in the state table.");
        }
    }

    public void ChangeState(string newStateName)
    {
        if (stateTable.ContainsKey(newStateName))
        {
            if (!string.IsNullOrEmpty(currentStateName))
            {
                stateTable[currentStateName].Exit();
            }
            currentStateName = newStateName;
            stateTable[currentStateName].Enter();
        }
        else
        {
            Debug.LogWarning("Cannot transition: State " + newStateName + " does not exist in the state table.");
        }
    }
}
