using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
  public int carrotCount = 10, EnemyCount = 0;
  public Transform spawnDenfenderPosition, spawnEnemyPosition;
  public GameObject[] UiLevel;
  public GameObject tower, gameResult;
  public GameObject rabbitWarrior, wolfNormal;
  public Text carrotCountText, timeText;
  bool winLoseCheck = false, timerStarted = false;
  private float time = 0;
  private int carrotPlusTimer = 0, defenderCount = 0;

  // Start is called before the first frame update
  void Start()
  {
  }

  // Update is called once per frame
  void Update()
  {
    if (carrotCountText.text != carrotCount.ToString())
      carrotCountText.text = carrotCount.ToString();
    if (winLoseCheck)
      WinLoseCheck();
  }

  private IEnumerator GameTimer()
  {
    for (float i = 0; timerStarted; i += Time.deltaTime)
    {
      if (i >= 1)
      {
        if (carrotPlusTimer++ >= 2)
        {
          carrotPlusTimer = 0;
          carrotCount++;
        }
        timeText.text = "Time : " + (int)(++time) + "s";
        i = 0;
      }
      yield return 0;
    }
  }

  public void StartLevel(int level)
  {
    carrotCount = 10;
    time = 0;
    timerStarted = true;
    carrotCountText.text = carrotCount.ToString();
    timeText.text = "Time : " + (int)time + "s";
    foreach (GameObject index in UiLevel)
    {
      if (index != null)
        index.SetActive(false);
    }
    gameResult.SetActive(false);
    UiLevel[level].SetActive(true);
    StartCoroutine(Level(level));
    StartCoroutine(GameTimer());
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
    winLoseCheck = true;
  }

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
      defenderCount++;
      defender.transform.position = RandomPosition(spawnDenfenderPosition.position);
      defender.name = "Defender_" + defenderCount;
      Instantiate(defender);
      StartCoroutine(SortCharacter());
    }
  }
  public void SpawnEnemy(int enemyType)
  {
    GameObject enemy = null;
    switch (enemyType)
    {
      default:
      case 1: enemy = wolfNormal; break;
    }

    EnemyCount++;
    enemy.transform.position = RandomPosition(spawnEnemyPosition.position);
    enemy.name = "Enemy_" + EnemyCount;
    Instantiate(enemy);
    StartCoroutine(SortCharacter());
  }

  Vector3 RandomPosition(Vector3 position)
  {

    int random = Random.Range(0, 4);
    float offset = 0f;
    switch (random)
    {
      case 0: offset = 0f; break;
      case 1: offset = 0.2f; break;
      case 2: offset = 0.4f; break;
      case 3: offset = 0.6f; break;
    }
    position.Set(position.x, position.y + offset, position.z);
    return position;
  }

  private IEnumerator SortCharacter()
  {
    yield return new WaitForSeconds(0.1f);
    GameObject[] characters;
    int sortIndex = 0;
    characters = GameObject.FindGameObjectsWithTag("Character");
    IOrderedEnumerable<GameObject> sortTemp = characters.OrderByDescending(gameObject => gameObject.transform.position.y);
    sortTemp.ThenBy(gameObject => gameObject.layer);
    characters = sortTemp.ThenBy(gameObject => gameObject.transform.position.x).ToArray();
    foreach (GameObject character in characters)
      character.GetComponent<Character>().ChangeSortId(sortIndex++);
  }

  private void WinLoseCheck()
  {
    bool endLevel = false;
    Text gameResultText = gameResult.GetComponentInChildren<Text>();
    if (tower == null)
    {
      Debug.Log("LOSE!");
      gameResultText.text = "LOSE!";
      endLevel = true;
    }
    else if (EnemyCount == 0)
    {
      Debug.Log("WIN!");
      gameResultText.text = "WIN!";
      endLevel = true;
    }

    if (endLevel)
    {
      gameResult.SetActive(true);
      GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
      foreach (GameObject character in characters) Destroy(character);
      foreach (GameObject index in UiLevel)
      {
        if (index != null)
          index.SetActive(false);
      }
      UiLevel[0].SetActive(true);
      timerStarted = winLoseCheck = false;
    }
  }
}
