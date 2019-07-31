///<remarks> why even I wrote all this --Jarl </remarks>

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;
//using DG.Tweening;

//namespace Assets.StateMachines
//{
//    public enum FloorState
//    {
//        JustSpawned,
//        Ready,
//        Active,
//        Dying
//    }

//    [RequireComponent(typeof(Renderer))]
//    public class FloorMachine : StateMachine<FloorState>
//    {
//        private bool activated;
//        private Action onActivate;

//        protected override FloorState GetDefaultState() => FloorState.JustSpawned;

//        protected override Dictionary<FloorState, Action> GetStateHandlers() =>
//            new Dictionary<FloorState, Action>()
//            {
//                { FloorState.JustSpawned, DoNothing }
//            };
        

//        protected override void InitStateMachine()
//        {
//            myMat = GetComponent<Renderer>().material;
//        }

//        void DoNothing() {}

//        Material myMat;
//        Color justSpawnedColor = Color.white;
//        Color readyColor = Color.gray;
//        Color activeColor = new Color(40, 40, 255);
//        Color deadColor = new Color(255, 255, 255, 0);
//        float colorChangeTime = 1f;
//        Sequence seq = MakeSeq(); // = DOTween.Sequence();

//        public static Sequence MakeSeq()
//        {
//            return DOTween.Sequence();
//        }
//        public bool IsSeqAlive => seq.IsActive();
//        public void AddTween(Tween t)
//        {
//        }

//        public void KillTweens()
//        {
//            seq.Kill();

//        }

//        public void ChangeColor(Color newColor) => ChangeColor(newColor, colorChangeTime, null);
//        public void ChangeColor(Color newColor, float changeSpeed, TweenCallback callback)
//        {
//            KillTweens();
//            var t = myMat.DOColor(newColor, changeSpeed);
//            if (callback != null) t.OnComplete(callback);
//            seq.Append(t);
//        }

//        public void ShowUp()
//        {

//        }



//        public void Death()
//        {
//            ChangeColor(deadColor, colorChangeTime, () => Destroy(gameObject));
//        }

//        public void Activate()
//        {
//            activated = true;
//            onActivate?.Invoke();
//            ChangeState(FloorState.Active);
//            ChangeColor(activeColor);
//        }



//        void OnTriggerEnter(Collider col)
//        {
//            if (currentState == FloorState.Ready)
//                if (col.CompareTag("Player"))
//                    Activate();
//        }
//    }
//}
