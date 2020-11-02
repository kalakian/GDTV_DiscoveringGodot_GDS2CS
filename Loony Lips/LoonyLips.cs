using Godot;
using System;

public class LoonyLips : Control
{
    public override void _Ready()
    {
        var prompts = new String[] { "Del", "ham", "mushroom", "tasty" };
        var story = "Once upon a time, {0} ate a {1} and {2} pizza, which was very {3}.";

        GetNode<Label>("VBoxContainer/DisplayText").Text = String.Format(story, prompts);
        GetNode<LineEdit>("VBoxContainer/HBoxContainer/PlayerText").GrabFocus();
    }

    public void OnPlayerTextTextEntered(String newText)
    {
        UpdateDisplayText(newText);
    }

    public void OnOkButtonPressed()
    {
        var newText = GetNode<LineEdit>("VBoxContainer/HBoxContainer/PlayerText").Text;
        UpdateDisplayText(newText);
    }

    public void UpdateDisplayText(String newText)
    {
        GetNode<Label>("VBoxContainer/DisplayText").Text = newText;
        GetNode<LineEdit>("VBoxContainer/HBoxContainer/PlayerText").Clear();
    }
}
