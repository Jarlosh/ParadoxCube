using Assets.Managers;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rotate))]
public class Player : MonoBehaviour
{
    public GameObject kernel;
    public ParticleSystem engine;

    private Transform engineTransform;
    
    private Material kernelMat;
    private Rotate chiller;


    Tween curMoveTween;
    Tween curScaleTween;


    readonly Queue<Vector3> plannedMoves = new Queue<Vector3>(3);
    private LayerMask layerMask;
    
    public bool isSwitching = false;

    
    public float moveSpeed = 10;
    [Range(0, 1)] 
        public float scaleChangePoint = 0.8f;
    [Range(float.Epsilon, 1)] 
        public float scaleCoefficient;


        
    public void Awake()
    {
        layerMask = LayerMask.GetMask("Wall");
        kernelMat = kernel.GetComponent<Renderer>().material;
        engineTransform = engine.transform;

        chiller = GetComponent<Rotate>();
        engine.transform.parent = null;
    }

    public void Update()
    {
        engineTransform.position = transform.position;
    }
    
    #region Controlled move
    public void MoveControlled(Vector3 direction)
    {
        if(IsReadyForNextMove)
            AllowedMove(direction);
        else if(!isSwitching)
            plannedMoves.Enqueue(direction);
    }

    void AllowedMove(Vector3 direction)
    {
        var dist = DistAllowed(direction);
        if (dist >= 1)
        {
            var target = new Ray(transform.position, direction).GetPoint(dist);
            MoveTo(target, dist);
        }
    }
    
    float DistAllowed(Vector3 direction)
    {
        var raycast = new RaycastHit();

        var ray = new Ray(transform.position, direction);
        if (!Physics.Raycast(ray, out raycast, 100f, layerMask, QueryTriggerInteraction.UseGlobal))
            return 0;
        else
            return Mathf.Floor(raycast.distance);
    }
    

    void MoveTo(Vector3 endpos, float distantion)
    {
        var moveDuration = distantion / moveSpeed;

        curScaleTween = MakeScaleTween(moveDuration, distantion);
        curMoveTween = transform.DOMove(endpos, moveDuration)
            .OnComplete(CompleteMove)
            .OnKill(CheckQueue)
            .SetEase(Ease.InQuad);
    }

    Sequence MakeScaleTween(float duration, float dist)
    {
        var scaleInDuration = duration * scaleChangePoint;
        var scaleOutDuration = duration - scaleInDuration;

        var seq = DOTween.Sequence();
        var initScale = kernel.transform.localScale.x;
        var newScale = Mathf.Pow(scaleCoefficient, dist/2);
        
        var scaleInTween = kernel.transform.DOScale(newScale, scaleInDuration);
        var scaleOutTween = kernel.transform.DOScale(initScale, scaleOutDuration);
        
        seq.Append(scaleInTween)
            .Append(scaleOutTween)
            .SetEase(Ease.InQuad);
        
        return seq;
    }

    #endregion
    
    
    #region Update state
    void CheckQueue()
    {
        if (plannedMoves.Count > 0)
            MoveControlled(plannedMoves.Dequeue());
    }

    private bool IsReadyForNextMove => curMoveTween == null;

    void CompleteMove()
    {
        //SetEmission(false);
        if(curMoveTween != null && curMoveTween.IsActive())
            curMoveTween.Complete();
        curMoveTween = null;
    }
    
    private void FlushPlannedMoves()
    {
        plannedMoves.Clear();
    }
    #endregion
    
    
    # region Switch level
    public void MoveToLevel(Vector3 startPos, float getToDuration, float getIntoDuration)
    {
        StartSwitching();
        var upper = transform.DOMove(startPos + Vector3.up, getToDuration);
        var lower = transform.DOMove(startPos, getIntoDuration);

        curMoveTween = DOTween.Sequence()
            .Append(upper)
            .Append(lower)
            .OnComplete(EndSwitching);

    }

    void StartSwitching()
    {
        isSwitching = true;
        CompleteMove();
        FlushPlannedMoves();
    }

    void EndSwitching()
    {
        isSwitching = false;
        CompleteMove();
    }
    # endregion
    

    # region Coloring materials
    
    public void SetBurstColor(Color color)
    {
        var main = engine.main;
        DOTween.To(() => main.startColor.color, v => main.startColor = v, color, 1);
        main.startColor = color;
    }
    
    public Color MakeDarkColor(Color color, float power = 1f/4)
    {
        float hue, sat, val;
        Color.RGBToHSV(color, out hue, out sat, out val);
        sat = Mathf.Max(0, sat - power/4);
        val = Mathf.Max(0, val - power);
        return Color.HSVToRGB(hue, sat, val);
    }
    
    public void ChangeColor(Color color, float duration = 1)
    {
        //var shellColor = color;
        //shellColor.a = ResMan.Instance.playerShellOpacity;

        //var kernelColor = color;

        //shellMat.DOColor(shellColor, duration);
        
        
        kernelMat.DOColor(color, duration);
        SetBurstColor(MakeDarkColor(color));
    }
    # endregion

    
    # region Chill state
    public void StartChill()
    {
        curMoveTween?.Kill();
        chiller.GoChill();
    }

    public void StopChill()
    {
        chiller.StopChill();
    }
    # endregion
}
