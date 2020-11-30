using Godot;
using System;

public class Player : KinematicBody2D
{
    // Called when the node enters the scene tree for the first time.

    Vector2 velocity = Vector2.Zero;

    public override void _PhysicsProcess(float delta)
    {
        Vector2 inputVector = Vector2.Zero;
        inputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        inputVector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");

        if(inputVector != Vector2.Zero){
            velocity = inputVector;
        }else{
            velocity = Vector2.Zero;
        }
        MoveAndCollide(velocity);
    }
}
