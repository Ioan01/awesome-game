using Godot;
using System;

public partial class health : item
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        this.Price = 20;
        this.Name = "Health";
        base._Ready();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        base._Process(delta);
    }

    public override void OnBuy(Node player)
    {
        QueueFree();

        (player as player).Hp += 1;
    }
}