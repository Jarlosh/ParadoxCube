//using UnityEngine;
//using System.Reflection;
//using System.Collections.Generic;

//namespace Assets.TouchStuff
//{
    
//    public class TouchCreator
//    {
//        static BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;
//        static Dictionary<string, FieldInfo> fields;

//        object touch;

//        public float deltaTime { get { return ((UnityEngine.Touch)touch).deltaTime; } set { fields["m_TimeDelta"].SetValue(touch, value); } }
//        public int tapCount { get { return ((UnityEngine.Touch)touch).tapCount; } set { fields["m_TapCount"].SetValue(touch, value); } }
//        public TouchPhase phase { get { return ((UnityEngine.Touch)touch).phase; } set { fields["m_Phase"].SetValue(touch, value); } }
//        public Vector2 deltaPosition { get { return ((UnityEngine.Touch)touch).deltaPosition; } set { fields["m_PositionDelta"].SetValue(touch, value); } }
//        public int fingerId { get { return ((UnityEngine.Touch)touch).fingerId; } set { fields["m_FingerId"].SetValue(touch, value); } }
//        public Vector2 position { get { return ((UnityEngine.Touch)touch).position; } set { fields["m_Position"].SetValue(touch, value); } }
//        public Vector2 rawPosition { get { return ((UnityEngine.Touch)touch).rawPosition; } set { fields["m_RawPosition"].SetValue(touch, value); } }

//        public Touch Create()
//        {
//            return (UnityEngine.Touch)touch;
//        }

//        public TouchCreator()
//        {
//            touch = new UnityEngine.Touch();
//        }

//        static TouchCreator()
//        {
//            fields = new Dictionary<string, FieldInfo>();
//            foreach (var f in typeof(UnityEngine.Touch).GetFields(flags))
//            {
//                fields.Add(f.Name, f);
//                //Debug.Log("name: " + f.Name); // Use this to find the names of hidden private fields
//            }
//        }
//    }

//}
