using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public float maxHealth = 10f;
    public float health = 10f;
    public Transform valueBar;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 theScale = valueBar.localScale;
        if (health > maxHealth)
            health = maxHealth;
        else if (health <= 0)
            health = 0;

        theScale.x = health / maxHealth;
        valueBar.localScale = theScale;
    }
}
