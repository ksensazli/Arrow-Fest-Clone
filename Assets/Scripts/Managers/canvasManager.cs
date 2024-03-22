using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class canvasManager : MonoBehaviour
{
    public GameObject _startScreen;
    public GameObject _failedScreen;
    public GameObject _finishScreen;
    public TMPro.TMP_Text _levelCount;
    [SerializeField] private RectTransform _tapImage;
    
    
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
        _tapImage.DOAnchorPosX(120, 1.5f).SetLoops(-1, LoopType.Yoyo);
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
        DOVirtual.DelayedCall(.1f, () => _startScreen.SetActive(false));
    }

    private void loadedScreen()
    {
        _levelCount.text = GameConfig.Instance.Levels[(PlayerPrefs.GetInt("Level"))].ToString().PartBefore('(');
    }

    private void failedScreen()
    {
        DOVirtual.DelayedCall(.2f, () => _failedScreen.SetActive(true));
    }

    private void finishScreen()
    {
        DOVirtual.DelayedCall(.2f, () => _finishScreen.SetActive(true));
    }
}
