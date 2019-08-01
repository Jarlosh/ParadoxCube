using DG.Tweening;
using TMPro;
using UnityEngine;

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
        public float fadeDuration = 1;
        

        public void Honk()
        {
            Debug.Log("Honk!");
            Make();
        }


        
        public void Make()
        {
            foreach (var textBox in textBoxes)
            {
                



            }
            
            
        }
        
        
        
    }
}