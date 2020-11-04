using Godot;
using System;
using System.Collections.Generic;

public class LoonyLips : Control
{
    List<String> _playerWords = new List<String>();
    Story _currentStory;

    Label _displayText;
    LineEdit _playerText;


    public override void _Ready()
    {
        GD.Randomize();

        SetCurrentStory();

        _displayText = GetNode<Label>("VBoxContainer/DisplayText");
        _playerText = GetNode<LineEdit>("VBoxContainer/HBoxContainer/PlayerText");
        _playerText.GrabFocus();

        _displayText.Text = "Welcome to Loony Lips\n";

        CheckPlayerWordsLength();
    }

    void SetCurrentStory()
    {
        var numStories = GetNode("StoryBook").GetChildCount();
        var selectedStory = (int)(GD.Randi() % numStories);
        _currentStory = GetNode("StoryBook").GetChild<Story>(selectedStory);
    }

    public void OnPlayerTextTextEntered(String newText)
    {
        AddToPlayerWords();
    }

    public void OnOkButtonPressed()
    {
        if (IsStoryDone())
        {
            GetTree().ReloadCurrentScene();
        }
        else
        {
            AddToPlayerWords();
        }
    }

    public void AddToPlayerWords()
    {
        _playerWords.Add(_playerText.Text);
        _displayText.Text = "";
        _playerText.Clear();
        CheckPlayerWordsLength();
    }

    public bool IsStoryDone()
    {
        return _playerWords.Count == _currentStory.Prompts.Length;
    }

    public void CheckPlayerWordsLength()
    {
        if (IsStoryDone())
        {
            EndGame();
        }
        else
        {
            PromptPlayer();
        }
    }

    public void TellStory()
    {
        _displayText.Text = String.Format(_currentStory.StoryText, _playerWords.ToArray());
    }

    public void PromptPlayer()
    {
        _displayText.Text += "May I have " + _currentStory.Prompts[_playerWords.Count] + " please?";
    }

    public void EndGame()
    {
        _playerText.QueueFree();
        GetNode<Label>("VBoxContainer/HBoxContainer/ButtonLabel").Text = "Again?";
        TellStory();
    }
}
