using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputAction pauseAction;
    private InputAction scrollAction;

    public void Start()
    {
        pauseAction = InputSystem.actions.FindAction("Pause");
        scrollAction = InputSystem.actions.FindAction("ScrollPost");
        pauseAction.performed += TogglePause;
        scrollAction.performed += Scroll;
    }

    public void OnDestroy()
    {
        pauseAction.performed -= TogglePause;
        scrollAction.performed -= Scroll;
    }

    private void TogglePause(InputAction.CallbackContext _)
    {
        GameManager.Instance.TogglePause();
    }

    private void Scroll(InputAction.CallbackContext _)
    {
        GameManager.Instance.Scroll();
    }
}
