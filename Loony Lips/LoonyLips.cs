using Godot;
using System;

public class LoonyLips : Control
{
    public override void _Ready()
    {
        var prompts = new String[] { "Del", "ham", "mushroom", "tasty" };
        var story = "Once upon a time, {0} ate a {1} and {2} pizza, which was very {3}.";

        GetNode<Label>("DisplayText").Text = String.Format(story, prompts);
    }
}
