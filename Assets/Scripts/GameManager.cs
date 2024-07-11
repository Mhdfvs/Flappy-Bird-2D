using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject countDownPage;
    public Text scoreText;
    public Text finalScoreText;
    public Text hightScoreText;
    public int score = 0;   
    bool gameOver = false;
    public GameObject flappyBird;
    public GameObject pipePrefab;
    int pipeCount;
    public int pipeGap;
    public Vector3 startPipePos;
    [SerializeField] float pipeMaxY;
    [SerializeField] float pipeMinY;
    Vector3 gameHandlerStartPos;
    public GameObject gameHandler;
    List<GameObject> PipeList=new List<GameObject>();
    public List<BoxCollider2D> ColliderArray = new List<BoxCollider2D>();
    public GameObject BgMusic;
    public bool GameOver()
    {
        return gameOver;
    }
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Time.timeScale = 0;
        hightScoreText.text = "HighScore:" + PlayerPrefs.GetInt("HighScore");
        gameHandlerStartPos = gameHandler.transform.position;
    }
    public void PlayBtnPressed()
    {
        flappyBird.GetComponent<PolygonCollider2D>().enabled = true;
        RetainCollidersPresent();
        DestroyPipes();
        score = 0;
        scoreText.text = "0";
        gameHandler.transform.position = gameHandlerStartPos;
        gameOver = false;
        BgMusic.SetActive(true);
        startPage.SetActive(false);
        gameOverPage.SetActive(false);
        countDownPage.SetActive(true);
        SetFlappyBird();
        PipeGenerator();

    }
    void RetainCollidersPresent()
    {
        for(int i = 0;i < ColliderArray.Count;i++)
        {
            if (ColliderArray[i] != null)
                ColliderArray[i].GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    void DestroyPipes()
    {
        for(int i = 0;i < PipeList.Count;i++)
            DestroyImmediate(PipeList[i], gameObject);
        PipeList.Clear();
        pipeCount = 0;    
    }
    void SetFlappyBird()
    {
        flappyBird.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        flappyBird.transform.position = TapController.startPos;
        flappyBird.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void GameOverFn()
    {
        gameOver = true;
        BgMusic.SetActive(false);
        gameOverPage.SetActive(true);
        finalScoreText.text = "Score:" + score;
        if(score>PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
    public void PipeGenerator()
    {
        pipeCount++;
        float pipeYValue = Random.Range(pipeMinY, pipeMaxY);
        GameObject currentGeneratedPipe = Instantiate(pipePrefab);
        currentGeneratedPipe.transform.position = new Vector3((startPipePos.x + pipeCount * pipeGap), pipeYValue, 0);
        PipeList.Add(currentGeneratedPipe);
        if (pipeCount>3)
        {
            Destroy(PipeList[pipeCount - 4]);
        }
    }
    private void Update()
    {
        if (!gameOver)
        {
            MoveObject();
        }
    }
    void MoveObject()
    {
        gameHandler.transform.position += new Vector3(Time.deltaTime, 0, 0);
    }
}