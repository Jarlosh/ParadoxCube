using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugObj : MonoBehaviour
{

	void Start ()
    {
        var c = GetComponent<Renderer>();
        c.enabled = false;
	}
}
