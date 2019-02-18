using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenderManger : MonoBehaviour
{
    public int carrotCount = 10;
    public Transform spawnPosition;
    public GameObject rabbitWarrior;
    public Text carrotCountView;
    public Text timeView;
    private float time = 0;
    int oldTime = 0;
    private int carrotPlusTimer;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time - oldTime >= 1)
        {
            oldTime = (int)time;
            carrotPlusTimer++;
        }

        if (carrotPlusTimer >= 2)
        {
            carrotPlusTimer = 0;
            carrotCount++;
        }

        carrotCountView.text = carrotCount.ToString();
        timeView.text = "Time : " + oldTime + "s";
    }

    public void SpawnDefender(int type)
    {
        switch (type)
        {
            case 0:
                {

                    if (carrotCount >= 3)
                    {
                        carrotCount -= 3;
                        rabbitWarrior.transform.position = spawnPosition.position;
                        Instantiate(rabbitWarrior);
                    }
                    break;
                }
        }
    }
}
