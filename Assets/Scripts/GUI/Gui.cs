using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Core;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GUI
{
    #region funny code
    //textBox.materialForRendering.DOFade(0, fadeDuration);
    //textBox.material.DOFade(0, fadeDuration)
    //                .SetEase(Ease.Linear);
    // IT KILLED DEFAULT MATERIAL OH --Jarl    
    //textBox is TextMeshProUGUI instance.
    #endregion
    public class Gui : MonoBehaviour
    {
        [Header("Fading")]
        public TextMeshProUGUI[] textBoxes;
        public Button[] buttons;
        public float fadeDuration = 1;
        
        
        private Dictionary<TextMeshProUGUI, Color> hiddenBoxes;
        private Dictionary<Button, Color> hiddenButtons;

        
        private void Awake()
        {
            hiddenBoxes = new Dictionary<TextMeshProUGUI, Color>();
            hiddenButtons = new Dictionary<Button, Color>();
        }



        public void StartGame()
        {
            Hide();
            StartCoroutine(GameStartRoutine(fadeDuration));
        }

        IEnumerator GameStartRoutine(float time)
        {
            MapMan.Instance.PrepareToStart();
            yield return new WaitForSeconds(time);
            MapMan.Instance.StartGame();
        }
        
        public void Hide()
        {
            foreach (var box in textBoxes) FadeOut(box, fadeDuration);
            foreach (var button in buttons) FadeOut(button, fadeDuration);
            Invoke(nameof(SwitchChildren), fadeDuration);
        }

        private void SwitchChildren()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i).gameObject;
                child.SetActive(!child.activeSelf);
            }
        }

        private void FadeOut(Button widget, float duration)
        {
            var mat = widget.image.material;
            hiddenButtons[widget] = mat.color;
            DOTween.To(() => mat.color, v => mat.color = v, Color.clear, duration);
        }
        
        private void FadeOut(TextMeshProUGUI widget, float duration)
        {
            hiddenBoxes[widget] = widget.faceColor;
            DOTween.To(() => widget.faceColor, v => widget.faceColor = v, Color.clear, duration);
        }

    }
}