using UnityEngine;

[CreateAssetMenu(fileName = "UserProfile", menuName = "Scriptable Objects/UserProfile")]
public class UserProfile : ScriptableObject
{
    public Sprite profilePic;
    public string userName;
    public int followerCount;
    public int followingCount;
    [Multiline] public string profileDescription;

    public string GetUserName()
    {
        Debug.Log(userName);
        return userName == "Random"
        ? PostConstructor.UserNameDatabase.GetRandomUserName()
        : userName;
    }
}
