using UnityEngine;

[CreateAssetMenu(fileName = "UserProfile", menuName = "Scriptable Objects/UserProfile")]
public class UserProfile : ScriptableObject
{
    public Sprite profilePic;
    public string userName;
    public int followerCount;
    public int followingCount;
    [Multiline] public string profileDescription;

}
