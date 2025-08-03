using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnCommentClicked(int optionIndex);

public class Comment : MonoBehaviour
{
	public int OptionIndex { get; set; } = -1;
	public event OnCommentClicked Clicked;

	[SerializeField, Header("Components")]
	private Image background;
	[SerializeField]
	private Button button;
	[SerializeField]
	private Image arrow;
	[SerializeField]
	private TextMeshProUGUI text;
	[SerializeField, Header("Properties")]
	private CommentColor color;
	[SerializeField]
	private CommentArrow arrowSide;
	[SerializeField, Tooltip("How fast the comment moves")]
	private float lambda;
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

	private Vector2 desiredPosition;
	private bool isClickableChoice = false;

	public void Start()
	{
		button.onClick.AddListener(() =>
		{
			if (!isClickableChoice) return;
			Clicked?.Invoke(OptionIndex);
			SetArrow(CommentArrow.Right);
		});
	}

	public void Update()
	{
		transform.position = Vector2.Lerp(
			gameObject.transform.position,
			desiredPosition,
			1 - Mathf.Exp(-lambda * Time.deltaTime)
		);
	}

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
				this.arrow.enabled = true;
				this.arrow.rectTransform.anchorMin = new Vector2(0f, 0.5f);
				this.arrow.rectTransform.anchorMax = new Vector2(0f, 0.5f);
				this.arrow.rectTransform.pivot = new Vector2(0f, 0.5f);
				this.arrow.rectTransform.localScale = new Vector3(1f, 1f, 1f);
				this.arrow.rectTransform.localPosition = arrowOffsetLeft;
				break;
			case CommentArrow.Right:
				this.arrow.enabled = true;
				this.arrow.rectTransform.anchorMin = new Vector2(1f, 0.5f);
				this.arrow.rectTransform.anchorMax = new Vector2(1f, 0.5f);
				this.arrow.rectTransform.pivot = new Vector2(1f, 0.5f);
				this.arrow.rectTransform.localScale = new Vector3(-1f, 1f, 1f);
				this.arrow.rectTransform.localPosition = arrowOffsetRight;
				break;
			case CommentArrow.None:
				this.arrow.enabled = false;
				break;
		}
	}

	public void SetPosition(Vector2 position) => desiredPosition = position;

	public void SetClickableChoice(bool isClickableChoice)
	{
		this.isClickableChoice = isClickableChoice;
		button.interactable = isClickableChoice;
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
	Right,
	None
}
