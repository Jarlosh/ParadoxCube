using System.Collections;
using Assets.Managers;
using UnityEngine;

namespace Managers
{
    public class BootStrapper : MonoBehaviour
    {
        public GameObject mapManager;
        public GameObject resManager;

        //public Transform mapCenter;
        //public Transform salvageTarget;
        //public Camera gameCamera;

        void Awake()
        {
            DontDestroyOnLoad(this);
            // WAKE UP U MONKEY, U'LL BE LATE FOR WORK 
            //if (!MapMan.IsReady) Instantiate(mapManager); 
            //if (!ResMan.IsReady) Instantiate(resManager);

            MakeUpScreen();
            StartSimulation();
        }

        void MakeUpScreen()
        {
            Screen.orientation = ScreenOrientation.Portrait;
            Screen.autorotateToPortrait = true;

            //var cam = FindObjectOfType<Camera>() as Camera;
        }


        void StartSimulation()
        {
            StartCoroutine(StartRoutine());
        
        }

        bool AreManagersWoke =>
            MapMan.IsReady && ResMan.IsReady;

//    void FinishManagersInit()
//    {
//        var mapMan = MapMan.Instance;
//        mapMan.mapCenter = mapCenter;
//        mapMan.salvageTarget = salvageTarget;
//        mapMan.gameCamera = gameCamera;
//    }

        IEnumerator StartRoutine()
        {
            if (!AreManagersWoke)
                yield return new WaitUntil(() => AreManagersWoke);

            //FinishManagersInit();
            MapMan.Instance.StartStuff();
        }


    }
}
