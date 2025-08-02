using System.Collections.Generic;
using UnityEngine;

// Spawns and populates the next post
public class PostConstructor : MonoBehaviour
{
    public static UserNames UserNameDatabase;

    [SerializeField]
    private UserNames userNameDatabase;
    [SerializeField]
    private PostObject postPrefab;
    [SerializeField]
    private Vector2 postOrigin = new(6f, -32f);
    [SerializeField]
    private float postSpacing = 64f + 540.64f; // 540.64f is the height of the post prefab

    private List<PostObject> posts = new();
    private int currentPostIndex = 0;

    public static string GetRandomUserName() => UserNameDatabase.GetRandomUserName();

    public void Awake()
    {
        UserNameDatabase = userNameDatabase;
    }

    public void Start()
    {
        GameManager.Instance.SetPostConstructor(this);
    }

    public PostObject GetCurrentPostObject() => currentPostIndex < posts.Count ? posts[currentPostIndex] : null;

    public void BuildPost(Post post)
    {
        PostObject postObject = Instantiate(
            postPrefab,
            gameObject.transform.position
                - new Vector3(0, (posts.Count - currentPostIndex) * postSpacing, 0)
                + new Vector3(postOrigin.x, postOrigin.y),
            Quaternion.identity,
            gameObject.transform
        );

        postObject.SetPost(post);
        postObject.SetDesiredPosition(postObject.transform.position);

        posts.Add(postObject);
    }

    public void Scroll()
    {
        currentPostIndex++;
        if (currentPostIndex >= posts.Count) GameManager.Instance.EndOfPosts();
        for (int i = 0; i < posts.Count; i++)
        {
            PostObject post = posts[i];
            post.SetDesiredPosition(new Vector2(
                gameObject.transform.position.x + postOrigin.x,
                gameObject.transform.position.y - (i - currentPostIndex) * postSpacing + postOrigin.y
            ));
        }
    }
}
