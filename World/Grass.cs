using Godot;
using System;

public class Grass : Node2D
{
    PackedScene packedScene = new PackedScene();
    public override void _Process(float delta)
    {
        if(Input.IsActionJustPressed("attack")){
            packedScene = (PackedScene)ResourceLoader.Load("res://Effects/GrassEffect.tscn");
            Node2D grassEffect = (Node2D)packedScene.Instance();
            var world = GetTree().CurrentScene;
            world.AddChild(grassEffect);
            grassEffect.GlobalPosition = GlobalPosition;
            QueueFree();
        }
    }
}
