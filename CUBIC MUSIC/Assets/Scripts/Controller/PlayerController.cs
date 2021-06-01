using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    TimingManager TheTimingManager;

    // Start is called before the first frame update
    void Start()
    {
        TheTimingManager = FindObjectOfType<TimingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //판정체크
            TheTimingManager.CheckTiming();
        }

    }
}
