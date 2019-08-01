using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotDuration = 10;
    public float normalizationDuration = 1;
    public Vector3 defaultDirection = Vector3.zero;
    private static readonly Vector3 FullRot = new Vector3(360, 360, 360);

    private Tween flipping;
    
    private void Flip()
    {
        
        flipping = transform.DORotate(FullRot, rotDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .OnComplete(Flip);
    }

    public void GoChill()
    {
        flipping?.Kill();
        Flip();
    }
    
    public void StopChill()
    {
        flipping?.Kill();
        NormalizeDirection();        
    }

    private void NormalizeDirection()
    {
        flipping = transform.DORotate(defaultDirection, normalizationDuration)
            .SetEase(Ease.Linear);
    }
    
}
