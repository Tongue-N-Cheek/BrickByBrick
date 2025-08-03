using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Post", menuName = "Scriptable Objects/Post")]
public class Post : ScriptableObject
{
    public DialogueTree userProfile;
    public Tags[] postTags;
    [Multiline] public string description;
    public Sprite postImage;
    public Commenter[] commenters;
    public int sanityChange;
    public bool pausesTime;
    public bool isBossPost;
}

[Serializable]
public struct Commenter
{
    public DialogueTree commentingUser;
    public string comment;
}

public enum Tags
{
    Any,
    Astrology,
    Aquarius,
    Cat,
    Jewelry,
    Gym,
    Crypto,
    HumbleBrag,
    Education,
    Motivational,
    AI,
    Advertisement,
    Brainrot,
    Food
}
