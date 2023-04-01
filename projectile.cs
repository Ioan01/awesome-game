using Godot;
using System;

public partial class projectile : Area2D
{
    [Signal]
    public delegate void explodedEventHandler(Vector2 origin);

    private const float speed = 1000;
    public Vector2 direction { get; set; }

    public override void _PhysicsProcess(double delta)
    {
        LookAt(GlobalTransform.Origin + direction);
        Position += direction * speed * (float)delta;
    }

    void onProjectileBodyEntered(CharacterBody2D body)
    {
        EmitSignal("explodedEventHandler", GlobalTransform.Origin);
        QueueFree();
    }
}