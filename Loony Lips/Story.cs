using Godot;
using System;

public class Story : Node
{
    [Export]
    public String[] Prompts = new String[] { };

    [Export]
    public String StoryText = ""; // Can't be called Story as it clashes with class name
}
