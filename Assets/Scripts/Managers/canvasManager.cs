using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class canvasManager : MonoBehaviour
{
    public GameObject _startScreen;
    public GameObject _failedScreen;
    public GameObject _finishScreen;
    [SerializeField] private GameObject _arrowController;
    [SerializeField] private TMPro.TMP_Text _arrowLevel;
    [SerializeField] private TMPro.TMP_Text _arrowCost;
    [SerializeField] private TMPro.TMP_Text _incomeLevel;
    [SerializeField] private TMPro.TMP_Text _incomeCost;
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

    private void Update()
    {
        _arrowLevel.text = "Level " + (PlayerPrefs.GetInt("Arrow") + 1).ToString();
        _arrowCost.text = "Cost " + (50 * (PlayerPrefs.GetInt("Level") * 0.1f + 1)).ToString();
        _incomeLevel.text = "Level " + PlayerPrefs.GetInt("Income").ToString();
        _incomeCost.text = "Cost " + (100 * (PlayerPrefs.GetInt("Level") * 0.1f + 1)).ToString();
    }

    private void startScreen()
    {
        DOVirtual.DelayedCall(.1f, () => _startScreen.SetActive(false)).OnComplete(() =>
        {
            _arrowController.SetActive(true);
        });
    }

    private void loadedScreen()
    {
        _levelCount.text = GameConfig.Instance.Levels[(PlayerPrefs.GetInt("Level"))].ToString().PartBefore('(');
    }

    private void failedScreen()
    {
        DOVirtual.DelayedCall(.2f, () => _failedScreen.SetActive(true)).OnComplete(() =>
        {
            _arrowController.SetActive(false);
        });
    }

    private void finishScreen()
    {
        DOVirtual.DelayedCall(.2f, () => _finishScreen.SetActive(true)).OnComplete(() =>
        {
            _arrowController.SetActive(false);
        });
    }
}
