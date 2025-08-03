using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PostObject : MonoBehaviour
{
	[Tooltip("How fast the post moves to its desired position when scrolling")]
	public float lambda = 10f;

	public Post Post { get; private set; }

	[SerializeField]
	private ProfileHeader header;
	[SerializeField]
	private Image postImage;
	[SerializeField]
	private TextMeshProUGUI description;

	private Vector2 desiredPosition;

	public void Update()
	{
		transform.position = Vector2.Lerp(
			gameObject.transform.position,
			desiredPosition,
			1 - Mathf.Exp(-lambda * Time.deltaTime)
		);
	}

	public void SetPost(Post post)
	{
		Post = post;
		header.SetProfilePic(post.userProfile.GetProfilePic());
		header.SetUsername(post.userProfile.GetUserName());
		postImage.sprite = post.postImage;
		description.text = post.description;
	}

	public void SetDesiredPosition(Vector2 position)
	{
		desiredPosition = position;
	}
}
