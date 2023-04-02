using Godot;
using System;

public partial class candle : item
{
    // Called when the node enters the scene tree for the first time.
    
    public override void OnEnter(Node node)
    {
        if (node.GetGroups().Contains("players"))
        {
            state.Darkness -= 0.1f;
            QueueFree();
        }
    }

    
}