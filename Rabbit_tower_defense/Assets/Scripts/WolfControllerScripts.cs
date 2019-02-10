using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfControllerScripts : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigi;
    float move = 0;
    public float moveSpeed = 1f;
    public bool facingRight = true;
    public float attackSpeed = 1f;
    public bool gotTarget = false;
    public Transform targetCheck;
    public LayerMask whatIsTarget;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody2D>();
        anim.SetFloat("Attack Speed", attackSpeed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gotTarget = Physics2D.OverlapBox(targetCheck.position, new Vector2(0.01f, 20), 0f, whatIsTarget);
        anim.SetBool("Target", gotTarget);
        if (gotTarget)
            move = 0;
        else
            move = moveSpeed;

        anim.SetFloat("Speed", Mathf.Abs(move));
        rigi.velocity = new Vector2(move, rigi.velocity.y);

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
