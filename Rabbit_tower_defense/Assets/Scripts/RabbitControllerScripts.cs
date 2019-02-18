using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitControllerScripts : Character
{
    public override void ChildStart() { }
    public override void ChildUpdate() { }
    public override void ChildFixedUpdate()
    {
        float way = anim.GetFloat("Take back or Pull out");
        // bool animationClipsName = anim.GetCurrentAnimatorStateInfo(0).IsName("RabbitAttack");
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("RabbitAttack") && way > 0)
            anim.SetFloat("Take back or Pull out", -Mathf.Abs(way));
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("RabbitIdle") && way < 0)
            anim.SetFloat("Take back or Pull out", Mathf.Abs(way));

    }

}