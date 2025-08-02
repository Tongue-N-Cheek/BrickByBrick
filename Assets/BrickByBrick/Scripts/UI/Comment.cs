using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Comment : MonoBehaviour
{
	[SerializeField, Header("Components")]
	private Image background;
	[SerializeField]
	private Image arrow;
	[SerializeField]
	private TextMeshProUGUI text;
	[SerializeField, Header("Properties")]
	private CommentColor color;
	[SerializeField]
	private CommentArrow arrowSide;
	[SerializeField, Header("Variants")]
	private Sprite commentBackgroundBlue;
	[SerializeField]
	private Sprite commentBackgroundPink;
	[SerializeField]
	private Sprite commentBackgroundYellow;
	[SerializeField]
	private Sprite commentArrowBlue;
	[SerializeField]
	private Sprite commentArrowPink;
	[SerializeField]
	private Sprite commentArrowYellow;
	[SerializeField, Header("Offsets")]
	private Vector2 arrowOffsetLeft = new(15.3f - 192f, 0); // Hardcoded offset
	[SerializeField]
	private Vector2 arrowOffsetRight = new(-120.3f + 192f, 0); // Hardcoded offset

#if UNITY_EDITOR
	public void OnValidate()
	{
		SetColor(color);
		SetArrow(arrowSide);
	}
#endif

	public void SetText(string text) => this.text.text = text;

	public void SetColor(CommentColor color)
	{
		this.color = color;

		switch (color)
		{
			case CommentColor.Blue:
				background.sprite = commentBackgroundBlue;
				arrow.sprite = commentArrowBlue;
				break;
			case CommentColor.Pink:
				background.sprite = commentBackgroundPink;
				arrow.sprite = commentArrowPink;
				break;
			case CommentColor.Yellow:
				background.sprite = commentBackgroundYellow;
				arrow.sprite = commentArrowYellow;
				break;
		}
	}

	public void SetArrow(CommentArrow arrow)
	{
		arrowSide = arrow;

		switch (arrow)
		{
			case CommentArrow.Left:
				this.arrow.rectTransform.anchorMin = new Vector2(0f, 0.5f);
				this.arrow.rectTransform.anchorMax = new Vector2(0f, 0.5f);
				this.arrow.rectTransform.pivot = new Vector2(0f, 0.5f);
				this.arrow.rectTransform.localScale = new Vector3(1f, 1f, 1f);
				this.arrow.rectTransform.localPosition = arrowOffsetLeft;
				break;
			case CommentArrow.Right:
				this.arrow.rectTransform.anchorMin = new Vector2(1f, 0.5f);
				this.arrow.rectTransform.anchorMax = new Vector2(1f, 0.5f);
				this.arrow.rectTransform.pivot = new Vector2(1f, 0.5f);
				this.arrow.rectTransform.localScale = new Vector3(-1f, 1f, 1f);
				this.arrow.rectTransform.localPosition = arrowOffsetRight;
				break;
		}
	}
}

public enum CommentColor
{
	Blue,
	Pink,
	Yellow
}

public enum CommentArrow
{
	Left,
	Right
}
