using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BossDialogue", menuName = "Scriptable Objects/BossDialogue")]
public class BossDialogue : ScriptableObject
{
    public DialogueSection[] sections;
    public DialogueSection[] success;
    public string[] failure;
}

[Serializable]
public struct DialogueSection
{
    public string[] leadupLines;
    public DialogueChoice[] choices;
}

[Serializable]
public struct DialogueChoice
{
    public string choice;
    public string[] responseLines;
    public int score;
    public Tags relatedTag;
    public int minimumTagReposts;
}
