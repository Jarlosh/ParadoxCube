using System;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    public Vector2 ReferenceResolution = new Vector2(9, 16);
    public Vector3 ZoomFactor = Vector3.one;


    [HideInInspector]
    public Vector3 OriginPosition;

    void Start()
    {
        OriginPosition = transform.position;
        UpdateResolution();
    }

    void Update()
    {
        //UpdateResolution();
    }

    void UpdateResolution()
    {
        if (ReferenceResolution.y == 0 || ReferenceResolution.x == 0)
            throw new ArgumentException();

        var refRatio = ReferenceResolution.x / ReferenceResolution.y;
        var ratio = (float)Screen.width / (float)Screen.height;

        transform.position = OriginPosition + transform.forward * (1f - refRatio / ratio) * ZoomFactor.z
                                            + transform.right * (1f - refRatio / ratio) * ZoomFactor.x
                                            + transform.up * (1f - refRatio / ratio) * ZoomFactor.y;

    }
}