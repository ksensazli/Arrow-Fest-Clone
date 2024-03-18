using DG.Tweening;
using UnityEngine;

public class arrowCounter : MonoBehaviour
{
    public const string ARROW_KEY = "Arrow";
    [SerializeField] private TMPro.TMP_Text _arrowCount;
    private int _initArrowAmount;
    private int _arrowAmount;

    private void OnEnable()
    {
        arrowController.onArrowCountChanged += OnArrowCountChanged;
        gameManager.onLevelLoaded += OnLevelLoaded;
    }

    private void OnDisable()
    {
        arrowController.onArrowCountChanged -= OnArrowCountChanged;
        gameManager.onLevelLoaded -= OnLevelLoaded;
    }

    private void OnArrowCountChanged(int amount)
    {
        _arrowAmount += amount;
        _arrowCount.text = _arrowAmount.ToString();
        DOTween.Kill(_arrowCount.transform);
        _arrowCount.transform.localScale = Vector3.one;
        _arrowCount.transform.DOPunchScale(Vector3.one * .25f, .15f)
            .OnComplete(()=>_arrowCount.transform.localScale = Vector3.one);
    }

    private void OnLevelLoaded()
    {
        _initArrowAmount = PlayerPrefs.GetInt(ARROW_KEY);
        _arrowAmount += _initArrowAmount;
        _arrowCount.text = _arrowAmount.ToString();
    }
}
