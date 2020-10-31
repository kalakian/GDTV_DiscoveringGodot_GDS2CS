using Godot;
using System;

public class HelloWorld : Node
{
    public override void _Ready()
    {
        var question = "What are we waiting for?";
        var answer = "GODOT!";

        GD.Print(question + " " + answer);
    }
}
