using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagesPanel : MonoBehaviour
{
	[SerializeField, Header("Components")]
	private CanvasGroup canvasGroup;
	[SerializeField]
	private Transform maskRectTransform;
	[SerializeField, Header("Comments")]
	private Comment commentPrefab;
	[SerializeField]
	private Vector2 commentOrigin = new(0f, 0f);
	[SerializeField]
	private float commentSpacing = 192f;

	private List<Comment> messages = new();
	private List<Comment> activeChoices = new();
	private bool isChoiceMade = false;
	private int choiceIndex = -1;

	public void Start()
	{
		GameManager.Instance.SetMessagesPanel(this);
		canvasGroup.alpha = 0f;
	}

	public void OnDestroy()
	{
		GameManager.Instance.SetMessagesPanel(null);
	}

	public void StartDialogue(BossDialogue dialogue)
	{
		StartCoroutine(ShowMultipleMessages(dialogue));
	}

	public void ShowMessage(string message, bool isSelfMessage = false, int optionIndex = -1)
	{
		Debug.Log(message);
		for (int i = 0; i < messages.Count; i++)
		{
			Comment c = messages[i];
			c.SetPosition(
				maskRectTransform.position
				+ (Vector3)commentOrigin
				+ commentSpacing * (messages.Count - i) * Vector3.up
			);
		}

		Comment comment = Instantiate(
			commentPrefab,
			maskRectTransform.position + (Vector3)commentOrigin,
			Quaternion.identity,
			maskRectTransform
		);
		comment.SetColor(isSelfMessage ? CommentColor.Blue : CommentColor.Yellow);
		comment.SetArrow(isSelfMessage ? CommentArrow.None : CommentArrow.Left);
		comment.SetText(message);
		comment.SetPosition(maskRectTransform.position + (Vector3)commentOrigin);
		comment.OptionIndex = optionIndex;

		messages.Add(comment);
		if (isSelfMessage)
		{
			comment.SetClickableChoice(true);
			comment.Clicked += ChoiceMade;
			activeChoices.Add(comment);
		}
	}

	public void ShowPanel()
	{
		canvasGroup.alpha = 1f;
	}

	public void HidePanel()
	{
		canvasGroup.alpha = 0f;

		foreach (Comment c in messages)
		{
			Destroy(c.gameObject);
		}
		messages.Clear();
	}

	public void TogglePanel()
	{
		canvasGroup.alpha = canvasGroup.alpha < 0.5f ? 1f : 0f;
	}

	private IEnumerator ShowMultipleMessages(BossDialogue dialogue)
	{
		// yield return new WaitForSeconds(.3f);
		foreach (DialogueSection section in dialogue.sections)
		{
			foreach (string line in section.leadupLines)
			{
				ShowMessage(line);
				int characterCount = line.Length;
				yield return new WaitForSeconds(characterCount * 0.08f);
			}

			if (section.choices.Length <= 0) continue;

			DialogueChoice[] shuffledChoices = section.choices;
			for (int i = 0; i < shuffledChoices.Length; i++)
			{
				int j = Random.Range(0, i + 1);
				(shuffledChoices[i], shuffledChoices[j]) = (shuffledChoices[j], shuffledChoices[i]);
			}

			for (int i = 0; i < shuffledChoices.Length; i++)
			{
				DialogueChoice choice = shuffledChoices[i];
				if (!GameManager.Instance.RepostedMinimum(choice.relatedTag, choice.minimumTagReposts)) continue;
				ShowMessage(choice.choice, true, i);
				yield return new WaitForSeconds(0.3f);
			}

			yield return new WaitUntil(() => isChoiceMade);

			foreach (string line in shuffledChoices[choiceIndex].responseLines)
			{
				ShowMessage(line);
				int characterCount = line.Length;
				yield return new WaitForSeconds(characterCount * 0.08f);
			}

			isChoiceMade = false;
			choiceIndex = -1;
		}

		bool wonAgainstBoss = GameManager.Instance.WonAgainstBoss();

		if (wonAgainstBoss)
		{
			foreach (DialogueSection section in dialogue.success)
			{
				foreach (string line in section.leadupLines)
				{
					ShowMessage(line);
					int characterCount = line.Length;
					yield return new WaitForSeconds(characterCount * 0.08f);
				}

				if (section.choices.Length <= 0) continue;

				for (int i = 0; i < section.choices.Length; i++)
				{
					DialogueChoice choice = section.choices[i];
					ShowMessage(choice.choice, true, i);
					yield return new WaitForSeconds(0.3f);
				}

				yield return new WaitUntil(() => isChoiceMade);

				foreach (string line in section.choices[choiceIndex].responseLines)
				{
					ShowMessage(line);
					int characterCount = line.Length;
					yield return new WaitForSeconds(characterCount * 0.08f);
				}

				isChoiceMade = false;
				choiceIndex = -1;
			}
		}
		else
		{
			foreach (string line in dialogue.failure)
			{
				ShowMessage(line);
				int characterCount = line.Length;
				yield return new WaitForSeconds(characterCount * 0.08f);
			}
		}

		yield return new WaitForSeconds(1f);
		HidePanel();
		GameManager.Instance.AdvanceStage();
	}

	public void ChoiceMade(int index)
	{
		Debug.Log("Choice made: " + index);
		choiceIndex = index;

		foreach (Comment comment in activeChoices)
		{
			comment.SetClickableChoice(false);
		}

		isChoiceMade = true;
	}
}
