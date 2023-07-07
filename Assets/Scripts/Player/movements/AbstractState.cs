using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractState
{
    protected PlayerMotor player;
    protected PlayerController controller;
    public string name = "abstract";

    public AbstractState(PlayerMotor player, PlayerController controller)
    {
        this.player = player;
        this.controller = controller;
    }
    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void Transition() { }
    public virtual void Exit() { }
}