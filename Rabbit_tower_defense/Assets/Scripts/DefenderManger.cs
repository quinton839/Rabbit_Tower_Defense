using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenderManger : MonoBehaviour
{
    public int carrotCount = 10;
    public Text carrotCountView;
    // Start is called before the first frame update
    void Start()
    {
        carrotCountView.text = carrotCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
