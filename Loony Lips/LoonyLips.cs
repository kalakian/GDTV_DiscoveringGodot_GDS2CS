using Godot;
using System;
using System.Collections.Generic;

public class LoonyLips : Control
{
    List<String> _playerWords = new List<String>();
    List<Dictionary<String, object>> _template = new List<Dictionary<String, object>>() {
        new Dictionary<string, object>(){
            { "prompts", new string[] { "a name", "a noun", "a noun", "an adverb" } },
            { "story", "Once upon a time, {0} ate a {1} and {2} pizza, which was very {3}." }
            },
        new Dictionary<string, object>(){
            { "prompts", new string[] { "a noun", "a name", "an adjective", "another noun", "another name" } },
            { "story", "There once was a {0} called {1} who searched far and wide for the mythical {2} {3} of {4}" }
            }
        };
    Dictionary<String, object> _currentStory;

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
        _currentStory = _template[(int)(GD.Randi() % _template.Count)];
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
        return _playerWords.Count == (_currentStory["prompts"] as string[]).Length;
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
        _displayText.Text = String.Format(_currentStory["story"] as string, _playerWords.ToArray());
    }

    public void PromptPlayer()
    {
        _displayText.Text += "May I have " + (_currentStory["prompts"] as string[])[_playerWords.Count] + " please?";
    }

    public void EndGame()
    {
        _playerText.QueueFree();
        GetNode<Label>("VBoxContainer/HBoxContainer/ButtonLabel").Text = "Again?";
        TellStory();
    }
}
