
//using UnityEngine;
//using System.Collections;

//namespace Assets.TouchStuff
//{

//    public struct SwipeAction
//    {
//        public SwipeDirection direction;
//        public Vector2 rawDirection;
//        public Vector2 startPosition;
//        public Vector2 endPosition;
//        public float startTime;
//        public float endTime;
//        public float duration;
//        public bool longPress;
//        public float distance;
//        public float longestDistance;

//        public override string ToString()
//        {
//            return string.Format("[SwipeAction: {0}, From {1}, To {2}, Delta {3}, Time {4:0.00}s]", direction, rawDirection, startPosition, endPosition, duration);
//        }
//    }

//    public enum SwipeDirection
//    {
//        None, // Basically means an invalid swipe
//        Up,
//        UpRight,
//        Right,
//        DownRight,
//        Down,
//        DownLeft,
//        Left,
//        UpLeft
//    }

//    public class SwipeManager : MonoBehaviour
//    {
//        public System.Action<SwipeAction> onSwipe;
//        public System.Action<SwipeAction> onLongPress;

//        [Range(0f, 200f)]
//        public float minSwipeLength = 100f;

//        public float longPressDuration = 0.5f;

//        Vector2 currentSwipe;
//        SwipeAction currentSwipeAction = new SwipeAction();

//        void Update()
//        {
//            DetectSwipe();
//        }

//        public void DetectSwipe()
//        {
//            var touches = InputHelper.GetTouches();
//            if (touches.Count > 0)
//            {
//                UnityEngine.Touch t = touches[0];

//                if (t.phase == TouchPhase.Began)
//                {
//                    ResetCurrentSwipeAction(t);
//                }

//                if (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary)
//                {
//                    UpdateCurrentSwipeAction(t);
//                    if (!currentSwipeAction.longPress && currentSwipeAction.duration > longPressDuration && currentSwipeAction.longestDistance < minSwipeLength)
//                    {
//                        currentSwipeAction.direction = SwipeDirection.None; // Invalidate current swipe action
//                        currentSwipeAction.longPress = true;
//                        if (onLongPress != null)
//                        {
//                            onLongPress(currentSwipeAction); // Fire event
//                        }
//                        return;
//                    }
//                }

//                if (t.phase == TouchPhase.Ended)
//                {
//                    UpdateCurrentSwipeAction(t);

//                    // Make sure it was a legit swipe, not a tap, or long press
//                    if (currentSwipeAction.distance < minSwipeLength || currentSwipeAction.longPress) // Didnt swipe enough or this is a long press
//                    {
//                        currentSwipeAction.direction = SwipeDirection.None; // Invalidate current swipe action
//                        return;
//                    }

//                    onSwipe?.Invoke(currentSwipeAction); // Fire event
//                }
//            }
//        }

//        void ResetCurrentSwipeAction(UnityEngine.Touch t)
//        {
//            currentSwipeAction.duration = 0f;
//            currentSwipeAction.distance = 0f;
//            currentSwipeAction.longestDistance = 0f;
//            currentSwipeAction.longPress = false;
//            currentSwipeAction.startPosition = new Vector2(t.position.x, t.position.y);
//            currentSwipeAction.startTime = Time.time;
//            currentSwipeAction.endPosition = currentSwipeAction.startPosition;
//            currentSwipeAction.endTime = currentSwipeAction.startTime;
//        }

//        void UpdateCurrentSwipeAction(UnityEngine.Touch t)
//        {
//            currentSwipeAction.endPosition = new Vector2(t.position.x, t.position.y);
//            currentSwipeAction.endTime = Time.time;
//            currentSwipeAction.duration = currentSwipeAction.endTime - currentSwipeAction.startTime;
//            currentSwipe = currentSwipeAction.endPosition - currentSwipeAction.startPosition;
//            currentSwipeAction.rawDirection = currentSwipe;
//            currentSwipeAction.direction = GetSwipeDirection(currentSwipe);
//            currentSwipeAction.distance = Vector2.Distance(currentSwipeAction.startPosition, currentSwipeAction.endPosition);
//            if (currentSwipeAction.distance > currentSwipeAction.longestDistance) // If new distance is longer than previously longest
//            {
//                currentSwipeAction.longestDistance = currentSwipeAction.distance; // Update longest distance
//            }
//        }

//        SwipeDirection GetSwipeDirection(Vector2 direction)
//        {
//            var angle = Vector2.Angle(Vector2.up, direction.normalized); // Degrees
//            var swipeDirection = SwipeDirection.None;

//            if (direction.x > 0) // Right
//            {
//                if (angle < 22.5f) // 0.0 - 22.5
//                {
//                    swipeDirection = SwipeDirection.Up;
//                }
//                else if (angle < 67.5f) // 22.5 - 67.5
//                {
//                    swipeDirection = SwipeDirection.UpRight;
//                }
//                else if (angle < 112.5f) // 67.5 - 112.5
//                {
//                    swipeDirection = SwipeDirection.Right;
//                }
//                else if (angle < 157.5f) // 112.5 - 157.5
//                {
//                    swipeDirection = SwipeDirection.DownRight;
//                }
//                else if (angle < 180.0f) // 157.5 - 180.0
//                {
//                    swipeDirection = SwipeDirection.Down;
//                }
//            }
//            else // Left
//            {
//                if (angle < 22.5f) // 0.0 - 22.5
//                {
//                    swipeDirection = SwipeDirection.Up;
//                }
//                else if (angle < 67.5f) // 22.5 - 67.5
//                {
//                    swipeDirection = SwipeDirection.UpLeft;
//                }
//                else if (angle < 112.5f) // 67.5 - 112.5
//                {
//                    swipeDirection = SwipeDirection.Left;
//                }
//                else if (angle < 157.5f) // 112.5 - 157.5
//                {
//                    swipeDirection = SwipeDirection.DownLeft;
//                }
//                else if (angle < 180.0f) // 157.5 - 180.0
//                {
//                    swipeDirection = SwipeDirection.Down;
//                }
//            }

//            return swipeDirection;
//        }
//    }

//}
