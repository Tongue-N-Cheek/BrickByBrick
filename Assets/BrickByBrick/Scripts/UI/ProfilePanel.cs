using TMPro;
using UnityEngine;

public class ProfilePanel : MonoBehaviour
{
	public ProfileHeader profileHeader;
	public TextMeshProUGUI bio;

	public void Start()
	{
		GameManager.Instance.SetProfilePanel(this);
	}

	public void SetProfile(UserProfile profile)
	{
		profileHeader.SetProfilePic(profile.profilePic);
		profileHeader.SetUsername(profile.userName);
		bio.text = profile.profileDescription;
	}
}
