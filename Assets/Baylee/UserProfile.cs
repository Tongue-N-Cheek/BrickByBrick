using UnityEngine;

[CreateAssetMenu(fileName = "UserProfile", menuName = "Scriptable Objects/UserProfile")]
public class UserProfile : ScriptableObject
{
    public UserNames userNameDatabase;
    public string userName;
    [Multiline] public string profileDescription;

    public void SetUserName()
    {
        if (userNameDatabase != null)
        {
            userName = userNameDatabase.GetRandomUserName();
        }
    }
}
