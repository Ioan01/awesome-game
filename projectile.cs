using Awesomegame;
using Godot;

public partial class projectile : Area2D
{
    private const float speed = 1500;
    public Vector2 direction { get; set; }

    public override void _PhysicsProcess(double delta)
    {
        LookAt(GlobalTransform.Origin + direction);
        Position += direction * speed * (float)delta;
    }

    public void _on_body_entered(Node2D node)
    {
        if (GlobalTransform == node.GlobalTransform || node is not character c) return;
        c.Hp--;
        c.KnockBack(direction);
        QueueFree();
    }
}