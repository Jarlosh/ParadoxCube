/// <remarks> Unity doesn't support Enum constraints...? </remarks>


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;

//namespace Assets.StateMachines
//{
//    public abstract class StateMachine<T> : MonoBehaviour
//        where T: Enum
//    {
        
//        protected Dictionary<T, Action> StateHandlers;
//        public T currentState;

//        protected abstract Dictionary<T, Action> GetStateHandlers();
//        protected abstract T GetDefaultState();

//        private void Start()
//        {
//            currentState = GetDefaultState();
//            StateHandlers = GetStateHandlers();
//            InitStateMachine();
//        }

//        protected abstract void InitStateMachine();

//        protected void Update()
//        {
//            HandleState();
//        }

//        protected void HandleState()
//        {
//            StateHandlers[currentState]();
//        }

//        protected void ChangeState(T newState)
//        {
//            if (!newState.Equals(currentState))
//                currentState = newState;
//        }
//    }
//}
