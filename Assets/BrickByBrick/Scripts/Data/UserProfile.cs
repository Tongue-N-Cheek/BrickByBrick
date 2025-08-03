using UnityEngine;

[CreateAssetMenu(fileName = "UserProfile", menuName = "Scriptable Objects/UserProfile")]
public class DialogueTree : ScriptableObject
{
    public Sprite profilePic;
    public string userName;
    public int followerCount;
    public int followingCount;
    [Multiline] public string profileDescription;

}
