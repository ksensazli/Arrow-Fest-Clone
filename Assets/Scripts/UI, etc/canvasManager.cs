using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class canvasManager : MonoBehaviour
{
    public GameObject _startScreen;
    public GameObject _infoScreen;
    public GameObject _failedScreen;
    public GameObject _finishScreen;
    public TMPro.TMP_Text _levelCount;
    
    public static canvasManager Instance { get; private set; }

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        gameManager.onLevelStart += startScreen;
        gameManager.onLevelLoaded += loadedScreen;
        gameManager.onLevelFailed += failedScreen;
        gameManager.onLevelCompleted += finishScreen;
        _startScreen.SetActive(true);
        Debug.Log("canvas on enable");
    }

    private void OnDisable()
    {
        gameManager.onLevelStart -= startScreen;
        gameManager.onLevelLoaded -= loadedScreen;
        gameManager.onLevelFailed -= failedScreen;
        gameManager.onLevelCompleted -= finishScreen;
    }

    private void startScreen()
    {
        DOVirtual.DelayedCall(.1f, () => _startScreen.SetActive(false)).OnComplete(() =>
        {
            _infoScreen.SetActive(true);
        });
    }

    private void loadedScreen()
    {
        _levelCount.text = GameConfig.Instance.Levels[(PlayerPrefs.GetInt("Level"))].ToString().PartBefore('(');
    }

    private void failedScreen()
    {
        DOVirtual.DelayedCall(.2f, () => _infoScreen.SetActive(false)).OnComplete(() =>
        {
            _failedScreen.SetActive(true);
        });
    }

    private void finishScreen()
    {
        DOVirtual.DelayedCall(.2f, () => _infoScreen.SetActive(false)).OnComplete(() =>
        {
            _finishScreen.SetActive(true);
        });
    }
}