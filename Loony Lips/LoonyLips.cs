using Godot;
using System;
using System.Collections.Generic;

public class LoonyLips : Control
{
    List<String> _playerWords = new List<String>();
    List<Story> _storybook;
    Story _currentStory;

    Label _displayText;
    LineEdit _playerText;


    public override void _Ready()
    {
        GD.Randomize();
        _storybook = LoadStoryBookFromJSON("StoryBook.json");
        SetCurrentStory();

        _displayText = GetNode<Label>("VBoxContainer/DisplayText");
        _playerText = GetNode<LineEdit>("VBoxContainer/HBoxContainer/PlayerText");
        _playerText.GrabFocus();

        _displayText.Text = "Welcome to Loony Lips\n";

        CheckPlayerWordsLength();
    }

    void SetCurrentStory()
    {
        var selectedStory = (int)(GD.Randi() % _storybook.Count);
        _currentStory = _storybook[selectedStory];
    }

    List<Story> LoadStoryBookFromJSON(String filename)
    {
        // Load the JSON file and convert to a Godot Array
        var file = new File();
        file.Open(filename, File.ModeFlags.Read);
        var text = file.GetAsText();
        var data = JSON.Parse(text).Result as Godot.Collections.Array;
        file.Close();

        List<Story> storybook = new List<Story>();
        // Convert all entries in the data Array to Stories
        foreach (Godot.Collections.Dictionary entry in data)
        {
            Story story = new Story();

            // Convert prompts from a Godot Array, and put into the Story instance
            var prompts = entry["prompts"] as Godot.Collections.Array;
            List<String> promptList = new List<string>();
            foreach (String prompt in prompts)
            {
                promptList.Add(prompt);
            }
            story.Prompts = promptList.ToArray();

            story.StoryText = entry["story"] as String;

            // Add the created story to the storybook
            storybook.Add(story);
        }

        return storybook;
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
