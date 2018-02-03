using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
    public Text gameText;
    private bool gameState;

    public Text levelDescriptionText;
    public string[] levelDescription;
    
    public GameObject ballPrefab;
    public GameObject[] levelMaps;
    public int[] levelMapsBrickN;

    private GameObject ball;
    private GameObject levelMap;
    private int level;
    private int brickN;


    // Use this for initialization
    void Start()
    {
        if (levelMaps.Length == 0)
            Debug.LogError("No level map.");
        if (levelMaps.Length != levelMapsBrickN.Length)
            Debug.LogError("The number of levelMaps and the number of levelMapsBrickN isn't equal.");

        gameText.text = "摁任意键开始游戏\nEsc键退出";
        levelDescriptionText.text = "";
        gameState = false;

    }

    // Update is called once per frame
    void Update()
    {
	if (Input.GetButton("Cancel"))
        {
            Application.Quit();
        }
        if (gameState)
        {
            if (brickN <= 0)
            {
                if (!EnterNextLevel())
                {
                    EndGame();
                }
            }
        }
        else
        {
            if(Input.anyKeyDown)
            {
                StartGame();
            }
        }
    }

    public void BrickNumberReduce()
    {
        brickN--;
    }

    bool EnterNextLevel()
    {
        if (level == levelMaps.Length)
        {
            level++;
            return false;
        }

        ball.GetComponent<Ball>().Active = false;
        Destroy(ball);
        Destroy(levelMap);

        ball = Instantiate(ballPrefab);
        levelMap = Instantiate(levelMaps[level]);
        brickN = levelMapsBrickN[level];

        if (levelDescription.Length > level)
            levelDescriptionText.text = levelDescription[level];
        else
            levelDescriptionText.text = "";

        level++;
        return true;
    }

    void StartGame()
    {
        ball = Instantiate(ballPrefab);
        levelMap = Instantiate(levelMaps[0]);
        brickN = levelMapsBrickN[0];
        level = 1;

        gameText.text = "";
        if (levelDescription.Length > 0)
            levelDescriptionText.text = levelDescription[0];
        else
            levelDescriptionText.text = "";
        gameState = true;
    }

    public void EndGame()
    {
        ball.GetComponent<Ball>().Active = false;
        Destroy(ball);
        Destroy(levelMap);
        if (level > levelMaps.Length)
            gameText.text = "恭喜你闯过了最后一关!\n摁任意键重新游戏\nEsc键退出";
        else
            gameText.text = "你死在了第 " + level.ToString() + " 关\n摁任意键重新游戏\nEsc键退出";
        levelDescriptionText.text = "";
        gameState = false;
    }
}
