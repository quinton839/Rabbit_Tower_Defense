using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject gobj;
    public Transform spawnPosition;
    int count = 0;
    void Start()
    {
        // InvokeRepeating("spawnEnemy",5f,1f);
    }

    void Update()
    {

    }

    public void spawnEnemy()
    {
        this.gobj.transform.position = spawnPosition.position;
        Instantiate(this.gobj);
        if(++count >= 10){
            CancelInvoke();
        }
    }
}
