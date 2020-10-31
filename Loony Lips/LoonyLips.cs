using Godot;
using System;

public class LoonyLips : Control
{
    public override void _Ready()
    {
        var prompts = new String[] { "Del", "ham", "mushroom", "tasty" };
        var story = "Once upon a time, {0} ate a {1} and {2} pizza, which was very {3}.";
        GD.Print(String.Format(story, prompts));

        prompts = new String[] { "Bob", "grape", "kiwi", "disgusting" };
        GD.Print(String.Format(story, prompts));
    }
}
