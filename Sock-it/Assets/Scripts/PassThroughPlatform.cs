using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassThroughPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;

    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            effector.rotationalOffset = 180f;
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            effector.rotationalOffset = 0f;
        }
    }
}
