using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PostObject : MonoBehaviour
{
	public Post Post { get; private set; }

	[SerializeField]
	private ProfileHeader header;
	[SerializeField]
	private Image postImage;
	[SerializeField]
	private TextMeshProUGUI description;

	public void SetPost(Post post)
	{
		Post = post;
		header.SetProfilePic(post.userProfile.profilePic);
		header.SetUsername(post.userProfile.userName);
		postImage.sprite = post.postImage;
		description.text = post.description;
	}
}
