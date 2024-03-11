using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nextLevelButton : MonoBehaviour
{
    [SerializeField] private Button _nextLevelButton;
    private void OnEnable()
    {
        _nextLevelButton.onClick.AddListener(()=>OnNextLevelButton());
    }

    private void OnDisable()
    {
        _nextLevelButton.onClick.RemoveAllListeners();
    }
    public void OnNextLevelButton()
    {
        gameManager.Instance.nextLevel();
        canvasManager.Instance._finishScreen.SetActive(false);
        canvasManager.Instance._startScreen.SetActive(true);
    }
}
