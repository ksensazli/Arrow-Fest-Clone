using DG.Tweening;
using UnityEngine;

public class arrowCounter : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _arrowCount;
    private int _arrowAmount;

    private void OnEnable()
    {
        arrowController.onArrowCountChanged += OnArrowCountChanged;
    }

    private void OnDisable()
    {
        arrowController.onArrowCountChanged -= OnArrowCountChanged;
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
}
