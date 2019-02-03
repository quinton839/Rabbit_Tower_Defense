using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : physicsObject
{
    // Start is called before the first frame update
    void Start()
    {
    }

    protected virtual void ComputeVeloity(){

    }


    // Update is called once per frame
    void Update()
    {
        
        ComputeVeloity();
    }
}
