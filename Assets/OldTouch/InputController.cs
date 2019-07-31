
//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;


//namespace Assets.TouchStuff
//{

//    [RequireComponent(typeof(SwipeManager))]
//    [RequireComponent(typeof(Player))]
//    public class InputController : MonoBehaviour
//    {
//        //public Player OurPlayer; // Perhaps your playerscript?
//        public Player player;


//        void Start()
//        {
//            SwipeManager swipeManager = GetComponent<SwipeManager>();
//            swipeManager.onSwipe += HandleSwipe;
//            swipeManager.onLongPress += HandleLongPress;
//            player = GetComponent<Player>();
//        }

//        Dictionary<SwipeDirection, Vector3> directions = new Dictionary<SwipeDirection, Vector3>()
//        {
//            {SwipeDirection.Up, Vector3.forward},
//            {SwipeDirection.Down, Vector3.back},
//            {SwipeDirection.Left, Vector3.left},
//            {SwipeDirection.Right, Vector3.right}
//        };

//        void HandleSwipe(SwipeAction swipeAction)
//        {
//            if (player != null)
//            {
//                if (directions.ContainsKey(swipeAction.direction))
//                    player.MoveControlled(directions[swipeAction.direction]);

//            }
//        }


//        //void HandleSwipe(SwipeAction swipeAction)
//        //{
//        //    Debug.LogFormat("HandleSwipe: {0}", swipeAction);
//        //}

//        void HandleLongPress(SwipeAction swipeAction)
//        {
//            Debug.LogFormat("HandleLongPress: {0}", swipeAction);
//        }
//    }

//}
