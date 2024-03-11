using DG.Tweening;
using UnityEngine;

public class goldManager : MonoBehaviour
{
    public const string GOLD_KEY = "Gold";
    [SerializeField] private TMPro.TMP_Text _goldText;
    [SerializeField] private int _goldAmount;
    private void OnEnable()
    {
        gold.OnGoldCollected += OnGoldCollected;
        _goldAmount = PlayerPrefs.GetInt(GOLD_KEY);
        _goldText.text = _goldAmount.ToString();
    }

    private void OnDisable()
    {
        gold.OnGoldCollected -= OnGoldCollected;
    }

    private void OnGoldCollected(int obj)
    {
        _goldAmount = PlayerPrefs.GetInt(GOLD_KEY);
        _goldAmount += obj;
        _goldText.text = _goldAmount.ToString();
        DOTween.Kill(_goldText.transform);
        _goldText.transform.localScale = Vector3.one;
        _goldText.transform.DOPunchScale(Vector3.one * .25f, .15f)
            .OnComplete(()=>_goldText.transform.localScale = Vector3.one);
        PlayerPrefs.SetInt(GOLD_KEY, _goldAmount);
    }
}


