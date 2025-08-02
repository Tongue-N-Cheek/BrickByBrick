using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Post", menuName = "Scriptable Objects/Post")]
public class Post : ScriptableObject
{
    public UserProfile userProfile;
    public Tags[] postTags;
    [Multiline] public string description;
    public Sprite postImage;
    public Commenter[] commenters;
}

[Serializable]
public struct Commenter
{
    public UserProfile commentingUser;
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
