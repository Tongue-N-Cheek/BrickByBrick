using UnityEngine;

[CreateAssetMenu(fileName = "UserProfile", menuName = "Scriptable Objects/UserProfile")]
public class UserProfile : ScriptableObject
{
    public Sprite profilePic;
    public string userName;
    public int followerCount;
    public int followingCount;
    [Multiline] public string profileDescription;

    public string GetUserName() => userName == "Random"
        ? PostConstructor.UserNameDatabase.GetRandomUserName()
        : userName;

    public Sprite GetProfilePic() => userName == "Random"
        ? PostConstructor.ProfilePicDatabase.GetRandomProfilePic()
        : profilePic;
}
