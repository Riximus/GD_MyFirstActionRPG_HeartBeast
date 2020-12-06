using Godot;
using System;

public class Grass : Node2D
{
    PackedScene packedScene = new PackedScene();

    public void CreateGrassEffect(){
        packedScene = (PackedScene)ResourceLoader.Load("res://Effects/GrassEffect.tscn");
        Node2D grassEffect = (Node2D)packedScene.Instance();
        var world = GetTree().CurrentScene;
        world.AddChild(grassEffect);
        grassEffect.GlobalPosition = GlobalPosition;
    }

    public void _on_Hurtbox_area_entered(Area2D area){
        CreateGrassEffect();
        QueueFree();
    }
}
