using System;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;

public class arrowController : MonoBehaviour
{
    public static Action OnArrowsFinished;
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _arrowObject;
    [SerializeField] private float _slideSpeed;
    [SerializeField] private float _sideBounds;
    [SerializeField] private float _lerpSpeed;
    [SerializeField] private TMPro.TMP_Text _goldText;
    [SerializeField] private Collider _arrowCollider;
    private int _collectedGold;
    private bool _isStart;
    private bool _isStopped;
    private SplineFollower _splineFollower;
    private Vector3 _forwardMoveAmount;
    private inputManager _inputManager;
    [SerializeField] private List<GameObject> _arrowList = new List<GameObject>();
    public int arrowCount => _arrowList.Count;
    
    public static arrowController Instance { get; private set; }

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        gameManager.onLevelStart += startGame;
        _inputManager = GetComponent<inputManager>();
        _splineFollower = GetComponentInParent<SplineFollower>();
        _splineFollower.followSpeed = 0;
        _collectedGold = 0;
        _goldText.text = _collectedGold.ToString();
    }

    private void OnDisable()
    {
        gameManager.onLevelStart -= startGame;
    }

    private void Update()
    {
        if (!_isStart)
        {
            return;
        }

        if (_isStopped)
        {
            return;
        }
        
        movePlayer();
    }

    public void disableCollider()
    {
        _arrowCollider.enabled = false;
        DOVirtual.DelayedCall(.6f, () => _arrowCollider.enabled = true);
    }

    private void startGame()
    {
        _splineFollower.followSpeed = 8;
        _isStart = true;
    }

    private void failedGame()
    {
        _isStopped = true;
        _splineFollower.follow = false;
     //   gameManager.onLevelFailed?.Invoke();
        gameManager.Instance.failedLevel();
    }

    public void arrowMultiply(int amount)
    {
        if (amount < 2)
            return;
        
        arrowSum((arrowCount) * (amount - 1));
    }

    public void arrowDivide(int amount)
    {
        if (amount < 1)
            return;
        
        //float reduceAmount = (arrowCount) * (amount - 1) / (float)amount;
        //Debug.Log("reduce amount is: " + reduceAmount);
        int targetAmount = arrowCount / amount;
        int reduceAmount = arrowCount - targetAmount;
        arrowMinus(reduceAmount);
    }

    public void arrowSum(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject arrowClone = Instantiate(_arrowObject, transform);
            _arrowList.Add(arrowClone);
        }
        
        circleArrow();
    }

    public void arrowMinus(int amount)
    {
        var reduceAmount = Math.Min(amount, arrowCount);
        for (int i = 0; i < reduceAmount; i++)
        {
            if (arrowCount<=0)
            {
                break;
            }
            GameObject arrowClone = _arrowList[^1 ];
            _arrowList.RemoveAt(_arrowList.Count - 1);
            Destroy(arrowClone);
        }
        //_arrowList.RemoveAt(_);
        
        if (arrowCount <= 0)
        {
            failedGame();
        }
        else
        {
          //  circleArrow();
        }
    }
    
    private void movePlayer()
    {
        Vector3 targetPosition = _player.transform.localPosition;

        if (Input.GetMouseButton(0))
        {
            targetPosition.x = 0;
            targetPosition.x = _inputManager.DragAmountX * _slideSpeed;
            targetPosition.x = Mathf.Clamp(targetPosition.x, -_sideBounds, _sideBounds);

            Vector3 targetPositionLerp = new Vector3(Mathf.Lerp(_player.localPosition.x, targetPosition.x, Time.fixedDeltaTime * _lerpSpeed),
                Mathf.Lerp(_player.localPosition.y, targetPosition.y, Time.fixedDeltaTime * _lerpSpeed),
                Mathf.Lerp(_player.localPosition.z, targetPosition.z, Time.fixedDeltaTime * _lerpSpeed));

            _player.localPosition = targetPositionLerp;
        }
    }

    private void circleArrow()
    {
        _arrowList[0].transform.localPosition = Vector3.zero;
        int arrowIndex = 1;
        int circleOrder = 1;
        
        while (true)
        {
            float radius = circleOrder * .1f;

            for (int i = 0; i < (circleOrder + 1) * 4; i++)
            {
                if (arrowIndex == arrowCount)
                {
                    return;
                }

                float radians = 2 * Mathf.PI / (circleOrder + 1) / 4 * i;
                float vertical = Mathf.Sin(radians);
                float horizontal = Mathf.Cos(radians);

                Vector3 dir = new Vector3(horizontal, vertical, 0f);
                Vector3 newPosition = dir * radius;

                GameObject _arrow = _arrowList[arrowIndex];

                if (_arrow != null)
                {
                    _arrow.transform.DOKill();
                    _arrow.transform.DOLocalMove(newPosition, 0.25f);
                }
                arrowIndex++;
            }
            circleOrder++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if (other.CompareTag("gold"))
        // {
        //     other.transform.DOScale(0f, 0.2f).OnComplete(() => other.gameObject.SetActive(false));
        //     _collectedGold++;
        //     _goldText.text = _collectedGold.ToString();
        // }

        if (other.CompareTag("endLine"))
        {
            _isStart = false;
            _splineFollower.follow = false;
            _player.DOLocalMoveX(0,1f,false);
            _player.DOMoveZ((_player.transform.position.z + (arrowCount / 5)), 3f, false);
        }
    }
}
