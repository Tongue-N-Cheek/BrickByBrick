using UnityEngine;

public class PostConstructor : MonoBehaviour
{
    public UserProfile testUserProfile;
    public Post postToConstruct;
    public int totalNumberOfPosts;

    void Start()
    {
        testUserProfile.SetUserName();
        Debug.LogWarning("UserName: " + testUserProfile.userName + " || Description: " + testUserProfile.profileDescription);
    }

    public void BuildPost()
    {

    }
}
