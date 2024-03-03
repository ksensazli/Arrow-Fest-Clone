using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
   [SerializeField] private Button _playButton;

   private void OnEnable()
   {
      _playButton.onClick.AddListener(()=>OnPlayButton());
   }

   private void OnDisable()
   {
      _playButton.onClick.RemoveAllListeners();
   }
   private void OnPlayButton()
   {
      gameManager.Instance.startLevel();
   }
}
