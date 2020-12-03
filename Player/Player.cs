using Godot;
using System;

public class Player : KinematicBody2D
{   
    const int ACCELERATION = 500;
    const int MAX_SPEED = 80;
    const int FRICTION = 500;
     Vector2 velocity = Vector2.Zero;

    public override void _PhysicsProcess(float delta)
    {
        Vector2 inputVector = Vector2.Zero;
        inputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        inputVector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
        inputVector = inputVector.Normalized();

        if(inputVector != Vector2.Zero){
            velocity = velocity.MoveToward(inputVector*MAX_SPEED, ACCELERATION*delta);
        }else{
            velocity = velocity.MoveToward(Vector2.Zero, FRICTION * delta);
        }
        velocity = MoveAndSlide(velocity);
    }
}
