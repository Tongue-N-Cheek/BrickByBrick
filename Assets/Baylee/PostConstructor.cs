using UnityEngine;

// Spawns and populates the next post
public class PostConstructor : MonoBehaviour
{
    public UserNames userNameDatabase;
    public UserProfile testUserProfile;
    public Post postToConstruct;
    public int totalNumberOfPosts;

    void Start()
    {
        Debug.LogWarning("UserName: " + testUserProfile.GetUserName() + " || Description: " + testUserProfile.profileDescription);

        BuildNextPost();
    }

    public void BuildNextPost()
    {
        if (postToConstruct == null)
        {
            Debug.LogError("AHHHHHH NO POST TO CONSTRUCT");
            return;
        }

        for (int i = 0; i < postToConstruct.commenters.Length; i++)
        {
            var commenter = postToConstruct.commenters[i];
            string username = commenter.commentingUser == null
                ? userNameDatabase.GetRandomUserName()
                : commenter.commentingUser.GetUserName();

            Debug.LogWarning("Commenter username: " + username + " || Comment: " + commenter.comment);
        }
    }
}
