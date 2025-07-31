using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Post", menuName = "Scriptable Objects/Post")]
public class Post : ScriptableObject
{
    public UserProfile userProfile;
    public string[] postTags;
    [Multiline] public string description;
    public Sprite postImage;
    public Commenter[] commenters;
    private bool isRepostedByPlayer = false;

    public void CreateComments()
    {
        
    }
}

[Serializable]
public struct Commenter
{
    public UserProfile commentingUser;
    public string comment;
}
