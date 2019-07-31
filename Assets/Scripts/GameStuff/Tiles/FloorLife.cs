using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FloorLife : MonoBehaviour
{

    [SerializeField] private Renderer frontRenderer;
    public Renderer backRenderer;
    Material frontMat;
    Material backMat;
    [Header("Colors")]
    public Color startColor = Color.gray / 8;
    public Color readyColor = Color.gray / 8;
    public Color activeColor = new Color(40f / 255, 40f / 255, 1);
    public Color deadColor = new Color(1, 1, 1, 0);
    
    const float colorChangeTime = 1f/2;

    bool canBeActivated = false;

    public Action onActivate;


    Tweener ChangeColor(Material material, Color newColor, float duration=colorChangeTime)
    {
        return material.DOColor(newColor, duration);
    }


    void Awake()
    {
        frontMat = frontRenderer.material;
        backMat = backRenderer.material;
        frontMat.color = startColor;
        backMat.color = startColor;
    }


    public void ShowUp()
    {
        ChangeColor(backMat, readyColor, 1);
        ChangeColor(frontMat, readyColor, 1);
        canBeActivated = true;
    }   

    void Death()
    {
        Destroy(gameObject);
    }

    public void Salvage()
    {
        ChangeColor(backMat, deadColor);
        ChangeColor(frontMat, deadColor)
            .OnComplete(Death);
    }

    void Activate()
    {
        canBeActivated = false;
        ChangeColor(frontMat, activeColor)
            .OnComplete(() => onActivate?.Invoke());
    }

    void OnTriggerEnter(Collider col)
    {
        if (canBeActivated)
            if (col.CompareTag("Player"))
                Activate();
    }

    
}
