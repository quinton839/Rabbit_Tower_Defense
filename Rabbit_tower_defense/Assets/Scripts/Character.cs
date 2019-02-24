using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Character : MonoBehaviour
{
  protected Animator anim;
  protected Rigidbody2D rigi;
  float move = 0;
  bool gotTarget = false;
  SpriteRenderer[] spriteList;
  public bool attacked = false;
  public bool facingRight = true;
  public float moveSpeed = 1f;
  public float attackSpeed = 1f;
  public Transform targetCheck;
  public LayerMask whatIsTarget;

  // Start is called before the first frame update
  void Start()
  {
    anim = GetComponent<Animator>();
    rigi = GetComponent<Rigidbody2D>();
    anim.SetFloat("Attack Speed", attackSpeed);

    spriteList = GetComponentsInChildren<SpriteRenderer>();

    ChildStart();
  }
  public virtual void ChildStart() { }

  // Update is called once per frame
  void Update()
  {
    ChildUpdate();
  }
  public virtual void ChildUpdate() { }

  void FixedUpdate()
  {
    gotTarget = Physics2D.OverlapBox(targetCheck.position, new Vector2(0.001f, 10), 0f, whatIsTarget);
    anim.SetBool("Target", gotTarget);
    if (gotTarget)
      move = 0;
    else
      move = facingRight ? moveSpeed : -moveSpeed;

    anim.SetFloat("Speed", Mathf.Abs(move));
    rigi.velocity = new Vector2(move, rigi.velocity.y);
    ChildFixedUpdate();
  }
  public virtual void ChildFixedUpdate() { }
  public void OnTriggerEnter2D(Collider2D other)
  {
    GameObject target;
    if (other.gameObject.transform.parent == null)
      target = other.gameObject;
    else
      target = other.gameObject.transform.parent.gameObject;
    int targetlayer = target.layer;
    bool matched = whatIsTarget % (1 << (targetlayer + 1)) / (1 << targetlayer) == 1;
    HealthController healthController = target.GetComponentInChildren<HealthController>();
    if (matched && healthController && !attacked)
    {
      healthController.health -= 1f;
      attacked = true;
    }
  }
  public void ChangeSortId(int sortIndex)
  {
    for (int i = 0; i < spriteList.Length; i++)
    {
      int sortingOrderTemp = spriteList[i].sortingOrder;
      sortingOrderTemp %= 10;
      spriteList[i].sortingOrder = sortingOrderTemp + sortIndex * 10;
    }
  }
  private void OnDestroy()
  {
    if (this.gameObject.layer == 8)
    {
      GameObject.Find("GameManager").GetComponentInChildren<GameManger>().EnemyCount--;
    }
  }
}