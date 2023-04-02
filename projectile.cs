using Awesomegame;
using Godot;

public partial class projectile : Area2D
{
    private static GlobalState state;
    
    private int enemyCnt = 0;
    
    private const float speed = 1500;
    public Vector2 direction { get; set; }

    public override void _Ready()
    {
        if (state is null)
            state = GetTree().CurrentScene.FindChild("globals") as GlobalState;
    }

    public override void _PhysicsProcess(double delta)
    {
        LookAt(GlobalTransform.Origin + direction);
        Position += direction * speed * (float)delta;
    }

    public void _on_body_entered(Node2D node)
    {
        if (GlobalTransform == node.GlobalTransform) return;
        if (node is character c)
        {
            var hit = GD.Load<PackedScene>("res://hit.tscn").Instantiate() as hit;
            hit.Position = node.Position;
            hit.Direction = direction;
            GetTree().CurrentScene.FindChild("map").AddChild(hit);
            
            c.Hp--;
            c.KnockBack(direction);
            enemyCnt++;
        }

        if (node is not character || enemyCnt == (int)(state.Enemeies / 1.5f))
            QueueFree();
    }
}