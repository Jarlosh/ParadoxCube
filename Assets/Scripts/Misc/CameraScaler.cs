using System;
using UnityEngine;

namespace Misc
{
    public class CameraScaler : MonoBehaviour
    {
        public Vector2 referenceResolution = new Vector2(9, 16);
        public Vector3 zoomFactor = Vector3.one;


        [HideInInspector]
        public Vector3 originPosition;

        void Start()
        {
            originPosition = transform.position;
            UpdateResolution();
        }

        void Update()
        {
            //UpdateResolution();
        }

        void UpdateResolution()
        {
            var refRatio = referenceResolution.x / referenceResolution.y;
            var ratio = (float)Screen.width / (float)Screen.height;

            var t = transform;
            t.position = originPosition + (1f - refRatio / ratio) * zoomFactor.z * t.forward
                                        + (1f - refRatio / ratio) * zoomFactor.x * t.right
                                        + (1f - refRatio / ratio) * zoomFactor.y * t.up;

        }
    }
}