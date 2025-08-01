using UnityEngine;

[CreateAssetMenu(fileName = "UserProfile", menuName = "Scriptable Objects/UserProfile")]
public class UserProfile : ScriptableObject
{
    public UserNames userNameDatabase;
    public string userName;
    public int followerCount;
    public int followingCount;
    [Multiline] public string profileDescription;

    public string GetUserName() =>
        string.IsNullOrEmpty(userName) ? userNameDatabase.GetRandomUserName() : userName;
}
