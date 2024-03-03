using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private Button _restartButton;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(()=>OnRestartButton());
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveAllListeners();
    }
    public void OnRestartButton()
    {
        gameManager.Instance.restartLevel();
    }
}
