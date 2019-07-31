using Assets.Managers;
using DG.Tweening;
using UnityEngine;

namespace Assets.Tiles
{
    [RequireComponent(typeof(Renderer))]
    public class WallLife : MonoBehaviour
    {
        static float morphSpeed = 1;
        static float blockSize = 1f;
        static float salvagedScale = blockSize / 8;



        //static Color salvagedColor = new Color(40, 40, 255, 128)/255;
        static float colorRange = 100/255;
        Color salvagedColor => MapMan.Instance.GetCurMainColor();

        Transform target;

        Color MakeSalvageColor() => MakeSalvageColor(colorRange);
        Color MakeSalvageColor(float delta)
        {
            var deltaColor = Color.white * Random.Range(-delta, delta);
            deltaColor.a = 0;
            var result = salvagedColor + deltaColor;
            for (var i = 0; i < 4; i++)
                result[i] = Mathf.Max(Mathf.Min(result[i], 1), 0);
            return result;
        }

        Vector3 MakeRotate()
        {
            return Random.rotation.eulerAngles;
        }

        void TurnOffColliders()
        {
            foreach (var col in GetComponents<Collider>())
                col.enabled = false;
        }

        public void Salvage(Transform targetTransform)
        {
            target = targetTransform;
            TurnOffColliders();
            var mat = GetComponent<Renderer>().material;

            var recolor = mat.DOColor(MakeSalvageColor(0.25f), morphSpeed);
            var rescale = transform.DOScale(salvagedScale, morphSpeed);
            var rotate = transform.DORotate(MakeRotate(), morphSpeed);

            DOTween.Sequence()
                .Append(recolor)
                .Join(rescale)
                .Join(rotate)
                .OnComplete(MoveToTarget);
        }

        public void AlternativeSalvage()
        {
            TurnOffColliders();
            var mat = GetComponent<Renderer>().material;
            var recolor = mat.DOColor(MakeSalvageColor(0.25f), morphSpeed);
            var rescale = transform.DOScale(0, morphSpeed);
            var rotate = transform.DORotate(MakeRotate(), morphSpeed);
            DOTween.Sequence()
                .Append(recolor)
                .Join(rescale)
                .Join(rotate)
                .OnComplete(Death);
        }

        public void HardSalvage()
        {   // well its look best
            Death();
            //TurnOffColliders();
            //var mat = GetComponent<Renderer>().material;
            //var newColor = mat.color;
            //newColor.a = 0;
            //mat.DOColor(newColor, morphSpeed * 2)
            //    .OnComplete(Death);
        }

        void MoveToTarget()
        {
            var t = MapMan.Instance.salvageTarget.position;
            transform.DOJump(t, 0, 1, morphSpeed)
                .SetEase(Ease.OutExpo, 0.5f)
                .OnComplete(Death);
        }

        void Death()
        {
            Destroy(gameObject);
        }


    }
}
