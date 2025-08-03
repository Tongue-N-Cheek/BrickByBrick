using UnityEngine;

[CreateAssetMenu(fileName = "ProfilePics", menuName = "Scriptable Objects/ProfilePics")]
public class ProfilePics : ScriptableObject
{
    public Sprite[] profilePics;

    public Sprite GetRandomProfilePic() => profilePics[Random.Range(0, profilePics.Length)];
}
