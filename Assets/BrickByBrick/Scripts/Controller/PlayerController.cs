using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputAction pauseAction;

    public void Start()
    {
        pauseAction = InputSystem.actions.FindAction("Pause");
        pauseAction.performed += TogglePause;
    }

    public void OnDestroy()
    {
        pauseAction.performed -= TogglePause;
    }

    private void TogglePause(InputAction.CallbackContext _)
    {
        GameManager.Instance.TogglePause();
    }
}
