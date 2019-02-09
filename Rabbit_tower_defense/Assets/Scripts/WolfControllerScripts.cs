using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfControllerScripts : MonoBehaviour
{
    public float maxSpeed = 10f;
    bool facingRight = true;
    Animator anim;
    Rigidbody2D rigi;
    public float jumpForce = 700f;
    public bool gotTarget = false;
    public Transform targetCheck;
    public LayerMask whatIsTarget;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gotTarget = Physics2D.OverlapBox(targetCheck.position, new Vector2(0.01f, 20), 0f, whatIsTarget);

        anim.SetFloat("vSpeed", rigi.velocity.y);

        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));
        rigi.velocity = new Vector2(move * maxSpeed, rigi.velocity.y);

        if (move > 0 && !facingRight)
            flip();
        else if (move < 0 && facingRight)
            flip();

    }
    void Update()
    {
        
    }
    void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
