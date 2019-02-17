using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Character
{
    public class Character : MonoBehaviour
    {
        Animator anim;
        Rigidbody2D rigi;
        float move = 0;
        bool gotTarget = false;
        public float moveSpeed = 1f;
        public float attackSpeed = 1f;
        public Transform targetCheck;
        public LayerMask whatIsTarget;
        public string whatIsLayer;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
            rigi = GetComponent<Rigidbody2D>();
            anim.SetFloat("Attack Speed", attackSpeed);

            SpriteRenderer[] spriteList = GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < spriteList.Length; i++)
                spriteList[i].sortingLayerName = whatIsLayer;

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            gotTarget = Physics2D.OverlapBox(targetCheck.position, new Vector2(0.001f, 10), 0f, whatIsTarget);
            anim.SetBool("Target", canAttack && gotTarget);
            if (gotTarget)
                move = 0;
            else
                move = -moveSpeed;

            anim.SetFloat("Speed", Mathf.Abs(move));
            rigi.velocity = new Vector2(move, rigi.velocity.y);

        }
        void Update()
        {

        }


        public void OnTriggerEnter2D(Collider2D other)
        {
            GameObject target;
            if (other.gameObject.transform.parent == null)
                target = other.gameObject;
            else
                target = other.gameObject.transform.parent.gameObject;
            int otherlayer = target.layer;
            bool matched = whatIsTarget % (1 << (otherlayer + 1)) / (1 << otherlayer) == 1;
            if (matched)
            {
                if (target.GetComponentInChildren<HealthController>())
                    target.GetComponentInChildren<HealthController>().health -= 1f;
            }
        }
    }
}