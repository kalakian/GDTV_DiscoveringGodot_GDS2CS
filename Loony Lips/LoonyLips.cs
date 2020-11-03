using Godot;
using System;
using System.Collections.Generic;

public class LoonyLips : Control
{
    List<String> _playerWords = new List<String>();
    String _story = "Once upon a time, {0} ate a {1} and {2} pizza, which was very {3}.";
    List<String> _prompts = new List<String>() { "a name", "a noun", "a noun", "an adverb" };

    Label _displayText;
    LineEdit _playerText;


    public override void _Ready()
    {
        _displayText = GetNode<Label>("VBoxContainer/DisplayText");
        _playerText = GetNode<LineEdit>("VBoxContainer/HBoxContainer/PlayerText");
        _playerText.GrabFocus();

        _displayText.Text = "Welcome to Loony Lips\n";

        CheckPlayerWordsLength();
    }

    public void OnPlayerTextTextEntered(String newText)
    {
        AddToPlayerWords();
    }

    public void OnOkButtonPressed()
    {
        AddToPlayerWords();
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
        return _playerWords.Count == _prompts.Count;
    }

    public void CheckPlayerWordsLength()
    {
        if (IsStoryDone())
        {
            TellStory();
        }
        else
        {
            PromptPlayer();
        }
    }

    public void TellStory()
    {
        _displayText.Text = String.Format(_story, _playerWords.ToArray());
    }

    public void PromptPlayer()
    {
        _displayText.Text += "May I have " + _prompts[_playerWords.Count] + " please?";
    }
}
