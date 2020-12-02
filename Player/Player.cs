using Godot;
using System;

public class Player : KinematicBody2D
{   
    const int ACCELERATION = 10;
    const int MAX_SPEED = 100;
    const int FRICTION = 10;
    Vector2 velocity = Vector2.Zero;

    public override void _PhysicsProcess(float delta)
    {
        Vector2 inputVector = Vector2.Zero;
        inputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        inputVector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
        inputVector = inputVector.Normalized();

        if(inputVector != Vector2.Zero){
            velocity += inputVector * ACCELERATION * delta;
            velocity = velocity.Clamped(MAX_SPEED * delta);
        }else{
            velocity = velocity.MoveToward(Vector2.Zero, FRICTION * delta);
        }
        MoveAndCollide(velocity);
    }
}
