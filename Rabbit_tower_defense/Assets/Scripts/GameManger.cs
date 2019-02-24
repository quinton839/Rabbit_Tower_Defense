using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
  public int carrotCount = 10;
  public Transform spawnDenfenderPosition;
  public Transform spawnEnemyPosition;
  public GameObject rabbitWarrior;
  public GameObject wolfNormal;
  public Text carrotCountView;
  public Text timeView;
  private float time = 0;
  private int carrotPlusTimer;
  // Start is called before the first frame update
  void Start()
  {
    carrotCountView.text = carrotCount.ToString();
    timeView.text = "Time : " + (int)time + "s";
  }

  // Update is called once per frame
  void Update()
  {
    if (Time.time - time >= 1)
    {
      time = Time.time;
      OneSecondTimer();
    }
    if (carrotPlusTimer >= 2)
    {
      carrotPlusTimer = 0;
      carrotCount++;
    }
    if (carrotCountView.text != carrotCount.ToString())
      carrotCountView.text = carrotCount.ToString();
  }

  void OneSecondTimer()
  {
    carrotPlusTimer++;
    timeView.text = "Time : " + (int)time + "s";
    SortCharacter();
  }

  public void StartLevel(int level)
  {
    StartCoroutine(Level(level));
  }

  IEnumerator Level(int level)
  {
    int[] enemyType = null;
    int time = 0;
    switch (level)
    {
      case 1:
        enemyType = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        break;
    }

    for (float i = 0; i <= enemyType.Length * 2 + 1; i += Time.deltaTime)
    {
      if (i - time >= 2)
      {
        SpawnEnemy(enemyType[time / 2]);
        time = (int)i;
      }
      yield return 0;
    }
  }
  int defenderCount = 0;
  public void SpawnDefender(int defenderType)
  {
    GameObject defender = null;
    bool enoughCarrot = false;
    switch (defenderType)
    {
      case 1:
        {
          if (carrotCount >= 3)
          {
            carrotCount -= 3;
            defender = rabbitWarrior;
            enoughCarrot = true;
          }
          break;
        }
    }
    if (enoughCarrot && defender != null)
    {
      defender.transform.position = RandomPosition(spawnDenfenderPosition.position);
      defender.name = "Defender_" + defenderCount;
      Instantiate(defender);
      defenderCount++;
    }
  }
  int EnemyCount = 0;
  public void SpawnEnemy(int enemyType)
  {
    GameObject enemy = null;
    switch (enemyType)
    {
      default:
      case 1: enemy = wolfNormal; break;
    }

    enemy.transform.position = RandomPosition(spawnEnemyPosition.position);
    enemy.name = "Enemy_" + EnemyCount;
    Instantiate(enemy);
    EnemyCount++;
  }

  Vector3 RandomPosition(Vector3 position)
  {

    int random = Random.Range(0, 3);
    float offset = 0f;
    switch (random)
    {
      case 0: offset = 0f; break;
      case 1: offset = 0.3f; break;
      case 2: offset = 0.6f; break;
    }
    position.Set(position.x, position.y + offset, position.z);
    return position;
  }

  void SortCharacter()
  {
    GameObject[] characters;
    characters = GameObject.FindGameObjectsWithTag("Character");
    IOrderedEnumerable<GameObject> sortTemp = characters.OrderBy(gameObject => gameObject.transform.position.y);
    sortTemp.ThenByDescending(gameObject => gameObject.layer);
    characters = sortTemp.ThenByDescending(gameObject => gameObject.transform.position.x).ToArray();
    foreach (GameObject character in characters)
    {
        // character.gameObject. ChangeSortId()
    }
  }
}
