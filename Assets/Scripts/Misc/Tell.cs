using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tell : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
	}

    bool last = false;


    float k()
    {
        var w = Screen.width;
        var h = Screen.height;
        var M = (float)Mathf.Max(w, h);
        var m = (float)Mathf.Min(w, h);
        return M / m;
    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.F))
            Debug.Log(k());
        //last = Input.GetKeyDown(KeyCode.F);
    }
}
