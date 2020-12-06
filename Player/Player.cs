using Godot;
using System;

public class Player : KinematicBody2D
{   
    const int ACCELERATION = 500;
    const int MAX_SPEED = 80;
    const int FRICTION = 500;
    enum states{
        MOVE, ROLL, ATTACK
    }

    states state = states.MOVE;
    Vector2 velocity = Vector2.Zero;
    public AnimationPlayer animationPlayer;
    public AnimationTree animationTree;
    public AnimationNodeStateMachinePlayback animationState;

    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        animationTree = GetNode<AnimationTree>("AnimationTree");
        animationState = (AnimationNodeStateMachinePlayback) animationTree.Get("parameters/playback");
        animationTree.Active = true;
    }

    public override void _PhysicsProcess(float delta)
    {
        switch(state){
            case states.MOVE:
                MoveState(delta);
                break;
            case states.ROLL:
                break;
            case states.ATTACK:
                AttackState(delta);
                break;
            default: 
                GD.Print("Player switch(state) Error: No State matched");
                break;
        }
    }

    public void MoveState(float delta){
        Vector2 inputVector = Vector2.Zero;
        inputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        inputVector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
        inputVector = inputVector.Normalized();

        if(inputVector != Vector2.Zero){
            animationTree.Set("parameters/Idle/blend_position", inputVector);
            animationTree.Set("parameters/Run/blend_position", inputVector);
            animationTree.Set("parameters/Attack/blend_position", inputVector);
            animationState.Travel("Run");

            velocity = velocity.MoveToward(inputVector*MAX_SPEED, ACCELERATION*delta);
        }else{
            animationState.Travel("Idle");

            velocity = velocity.MoveToward(Vector2.Zero, FRICTION * delta);
        }
        velocity = MoveAndSlide(velocity);

        if(Input.IsActionJustPressed("attack"))
            state = states.ATTACK;
    }
    public void AttackState(float delta){
        velocity = Vector2.Zero;
        animationState.Travel("Attack");
    }
    public void AttackAnimationFinished(){
        state = states.MOVE;
    }
}
