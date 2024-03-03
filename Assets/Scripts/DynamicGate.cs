using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DynamicGate : gateManager
{
   private float _durationTime;
   protected override void OnEnable()
   {
    base.OnEnable();
     _durationTime = Random.Range(1f, 3f);
     if (transform.localPosition.x > 0)
     {
         transform.DOLocalMoveX(-1f, _durationTime).SetLoops(-1, LoopType.Yoyo);
     }
     else
     {
         transform.DOLocalMoveX(1f, _durationTime).SetLoops(-1, LoopType.Yoyo);
     }
   }

   // protected override void Start()
   // {
   //     base.Start();
   //    
   // }
}
