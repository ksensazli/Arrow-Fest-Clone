using System;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;

public class arrowController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _arrowObject;
    [SerializeField] private float _slideSpeed;
    [SerializeField] private float _sideBounds;
    [SerializeField] private float _lerpSpeed;
    [SerializeField] private Collider _arrowCollider;
    private bool _isStart;
    private bool _isStopped;
    private bool _isEndLineReached;
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
        
        circleArrow(1);
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
        
        if (arrowCount <= 0 && !_isEndLineReached)
        {
            failedGame();
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

    private void circleArrow(int forEndVertical)
    {
        CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
        
        _arrowList[0].transform.localPosition = Vector3.zero;
        int arrowIndex = 1;
        int circleOrder = 1;
        
        while (true)
        {
            float radius = circleOrder * .1f;
            capsuleCollider.radius = 0.14f * circleOrder;
            for (int i = 0; i < (circleOrder + 1) * 4; i++)
            {
                if (arrowIndex == arrowCount)
                {
                    return;
                }

                float radians = 2 * Mathf.PI / (circleOrder + 1) / 4 * i;
                float vertical = Mathf.Sin(radians);
                float horizontal = Mathf.Cos(radians);

                Vector3 dir = new Vector3(horizontal, vertical / forEndVertical, 0f);
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
        if (other.CompareTag("endLine"))
        {
            _isStart = false;
            _isEndLineReached = true;
            _splineFollower.follow = false;
            circleArrow(4);
            _player.DOLocalMoveX(0,1f,false);
            _player.DOMoveZ((_player.transform.position.z + (arrowCount / 5)), 3f, false);
        }
    }
}
