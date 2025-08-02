using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileHeader : MonoBehaviour
{
	[SerializeField]
	private Image profilePic;
	[SerializeField]
	private TextMeshProUGUI username;

	public void SetProfilePic(Sprite profilePic) => this.profilePic.sprite = profilePic;
	public void SetUsername(string username) => this.username.text = $"@{username}";
}
