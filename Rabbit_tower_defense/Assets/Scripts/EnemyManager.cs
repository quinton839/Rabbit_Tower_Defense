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
        InvokeRepeating("spawnEnemy", 5f, 1f);
    }

    void Update()
    {

    }

    public void spawnEnemy()
    {
        Vector3 tempPosition = spawnPosition.position;
        int random = Random.Range(0, 3);
        float offset = 0f;
        switch ( random )
        {
            case 0: offset = 0f; break;
            case 1: offset = 0.3f; break;
            case 2: offset = 0.6f; break;
        }
        tempPosition.Set(tempPosition.x, tempPosition.y + offset, tempPosition.z);
        gobj.transform.position = tempPosition;
        Instantiate(gobj);
        if (++count >= 10)
        {
            CancelInvoke();
        }
    }
}
