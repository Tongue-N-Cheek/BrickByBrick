public class ButtonPlay : ButtonSceneChanger
{

	public override void Start()
	{
		button.onClick.AddListener(() =>
		{
			GameManager.Instance.GoToScene("S_Game");
			GameManager.Instance.Play();
		});
	}
}
