using System;
using UnityEngine;

namespace Assets.Managers
{
    public abstract class AbcManager<T> : MonoBehaviour
      where T : AbcManager<T>
    {
        private static T instance;
        private static bool isReady = false;

        public static T Instance
        {
            get
            {
                if (instance == null)
                    throw new Exception(string.Format("manager doesn't exist"));
                return instance;
            }
        }

        public static bool IsReady => isReady;


        void Awake()
        {
            SingleInit();
        }

        protected void SingleInit()
        {


            if (instance == null)
            {
                instance = this as T;
                isReady = true;
                DontDestroyOnLoad(gameObject);
                InitializeManager();
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

        }

        protected abstract void InitializeManager();
    }
}
