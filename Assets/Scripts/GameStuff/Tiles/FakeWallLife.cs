using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeWallLife : MonoBehaviour
{
    
    void Death()
    {
        Destroy(gameObject);
    }

    public void Salvage()
    {
        transform.DOScaleY(0, 0.1f)
            .OnComplete(Death);
    }
}
