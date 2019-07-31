using Assets.Managers;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1f;
    public bool isSwitching = false;

    public GameObject shell;
    public GameObject kernel;
    public GameObject engine;
    //public ParticleSystem emitter;



    Tween move;
    Queue<Vector3> planned_moves = new Queue<Vector3>(3);
    LayerMask layerMask;
    private Material shellMat;
    private Material kernelMat;
    
    public void Awake()
    {
        layerMask = LayerMask.GetMask("Wall");
        shellMat = shell.GetComponent<Renderer>().material;
        kernelMat = kernel.GetComponent<Renderer>().material;
        //SetEmission(false);
        //emitter.Stop();
    }

    public void MoveControlled(Vector3 direction)
    {
        if(IsReadyForNextMove)
            AllowedMove(direction);
        else if(!isSwitching)
            planned_moves.Enqueue(direction);
    }

    void CheckQueue()
    {
        if (planned_moves.Count > 0)
            MoveControlled(planned_moves.Dequeue());
    }

    bool IsReadyForNextMove => move == null;

    void AllowedMove(Vector3 direction)
    {
        var dist = DistAllowed(direction);
        if (dist >= 1)
        {
            var target = new Ray(transform.position, direction).GetPoint(dist);
            MoveTo(target, dist / speed);
        }
    }

    public float DistAllowed(Vector3 direction)
    {
        var raycast = new RaycastHit();

        var ray = new Ray(transform.position, direction);
        if (!Physics.Raycast(ray, out raycast, 100f, layerMask, QueryTriggerInteraction.UseGlobal))
            return 0;
        else
            return Mathf.Floor(raycast.distance);
    }


    void MoveTo(Vector3 endpos, float duration)
    {
        engine.transform.LookAt(endpos);
        //SetEmission(true);
        
        move = transform.DOMove(endpos, duration)
            .OnComplete(CompleteMove)
            .OnKill(CheckQueue)
            .SetEase(Ease.InQuad);
    }

    void CompleteMove()
    {
        //SetEmission(false);
        if(move != null && move.IsActive())
            move.Complete();
        move = null;
    }


    public void ChangeColor(Color color, float duration = 1)
    {
        var shellColor = color;
        shellColor.a = ResMan.Instance.playerShellOpacity;

        var kernelColor = color;

        shellMat.DOColor(shellColor, duration);
        kernelMat.DOColor(kernelColor, duration);
    }



    public void MoveToLevel(Vector3 startPos)
    {
        StartSwitching();
        var upper = transform.DOMove(startPos + Vector3.up, .5f);
        var lower = transform.DOMove(startPos, .25f);

        move = DOTween.Sequence()
            .Append(upper)
            .Append(lower)
            .OnComplete(EndSwitching);

    }


    private void FlushPlannedMoves()
    {
        planned_moves.Clear();
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
}
