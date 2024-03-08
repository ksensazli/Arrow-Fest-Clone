using System;
using DG.Tweening;
using UnityEngine;

public class gold : MonoBehaviour
{
  public static Action<int> OnGoldCollected;
  [SerializeField] private int _goldAmount;
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("arrow"))
    {
     DOTween.Sequence()
       .Append(transform.DOMoveY(transform.position.y+1f, 0.2f))
       .Append(transform.DOScale(0f, 0.2f))
       .OnComplete(() => gameObject.SetActive(false));
     
      OnGoldCollected?.Invoke(_goldAmount);
    }
  }
}
