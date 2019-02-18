using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfControllerScripts : Character
{
    public override void ChildStart() { }
    public override void ChildUpdate()
    {
    }
    public override void ChildFixedUpdate()
    {
        string current = anim.runtimeAnimatorController.animationClips[0].name;
        // float time = anim.runtimeAnimatorController.animationClips[0].;
    }

}
