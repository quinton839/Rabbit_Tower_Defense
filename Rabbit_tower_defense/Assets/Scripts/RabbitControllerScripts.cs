using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitControllerScripts : Character
{
  public override void ChildStart() { 
    GameObject.Find("CarrotAttack").SetActive(false);
  }
  public override void ChildUpdate() { }
  public override void ChildFixedUpdate()
  {
    
  }
}