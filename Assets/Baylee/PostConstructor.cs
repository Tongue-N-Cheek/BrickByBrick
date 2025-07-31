using Unity.VisualScripting;
using UnityEngine;

// Spawns and populates the next post
public class PostConstructor : MonoBehaviour
{
    public UserProfile testUserProfile;
    public Post postToConstruct;
    public int totalNumberOfPosts;

    void Start()
    {
        testUserProfile.SetUserName();
        Debug.LogWarning("UserName: " + testUserProfile.userName + " || Description: " + testUserProfile.profileDescription);

        BuildNextPost();
    }

    public void BuildNextPost()
    {
        if (postToConstruct.commenters != null)
        {
            for (int i = 0; i < postToConstruct.commenters.Length; i++)
            {
                var commenter = postToConstruct.commenters[i];
                // Sets the user profile to a new, blank profile if one isn't set by designers
                if (commenter.commentingUser == null)
                {
                    commenter.commentingUser = (UserProfile)ScriptableObject.CreateInstance(typeof(UserProfile));
                }
                commenter.commentingUser.GetUserName();

                Debug.LogWarning("Commenter username: " + commenter.commentingUser.userName + " || Comment: " + commenter.comment);
            }
        }
        else Debug.LogError("AHHHHHH NO COMMENTERS EXIST");
    }
}
