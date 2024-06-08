using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public GameObject stone;
    private MinigameStoneController stoneControllerScript;
    private ObjectPool objectPool;

    public float spawnInterval = 2.0f;
    public Transform[] spawnPoints;
    public ObstacleType[] obstacleTypes;

    //Life
    public RawImage stoneLifeImage;
    private RawImage[] stoneLifeImages;
    public GameObject lifeTransform;
    private int oldLife;

    //UI Panel
    public GameObject startPanel;
    public GameObject endPanel;
    public GameObject clearPanel;
    public Button startBtn;
    public Button restartBtn;
    public Button backGameBtn;

    //Game state
    public bool isGameOver;
    public bool minigameControl;
    public float gameTime = 10f;
    private float spawnTimer;
    private float gameTimer;

    //Progress Bar
    public GameObject checkLocation;
    public Image barUI;
    public Image locationUI;

    void Start()
    {
        stoneControllerScript = stone.GetComponent<MinigameStoneController>();
        objectPool = this.GetComponent<ObjectPool>();
        GameManager.Instance._minigame = this;
        
        minigameControl = false;

        startBtn.onClick.AddListener(StartGame);
        restartBtn.onClick.AddListener(RestartGame);
        backGameBtn.onClick.AddListener(GotoSauna);
    }

    void Update()
    {
        UpdateLifeImages();

        if(minigameControl)
        {
            if(!isGameOver)
            {
                UpdateTimers();
                CheckProgress();

                if (spawnTimer >= spawnInterval)
                {
                    SpawnObstacle();
                    spawnTimer = 0;
                }

                if (gameTimer > gameTime)
                    EndGame(true);
            }else
                EndGame(false);
        }
    }

    private void UpdateTimers()
    {
        spawnTimer += Time.deltaTime;
        gameTimer += Time.deltaTime;
    }

    private void UpdateLifeImages()
    {
        if (oldLife > stoneControllerScript.life)
        {
            oldLife = stoneControllerScript.life;
            if (oldLife < 0) oldLife = 0;
            for (int i = oldLife; i < stoneLifeImages.Length; i++)
                stoneLifeImages[i].enabled = false;
        }
    }

    private void SpawnObstacle()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        ObstacleType obstacleType = obstacleTypes[randomIndex];
        GameObject obstacle = objectPool.GetPooledObject(obstacleType);
        if (obstacle != null)
        {
            Transform spawnPoint = spawnPoints[randomIndex];
            obstacle.transform.position = spawnPoint.position;
            obstacle.SetActive(true);
        }
    }

    private void CheckProgress()
    {
        float progress = gameTimer / gameTime;
        float xPosition = Mathf.Lerp(0, 890, progress);
        locationUI.rectTransform.localPosition = new Vector2(xPosition, locationUI.rectTransform.localPosition.y);
    }

    private void SetData()
    {
        stone.transform.position = stoneControllerScript.startPos;

        minigameControl = true;

        checkLocation.SetActive(true);
        Vector2 startPos = new Vector2(0, locationUI.rectTransform.anchoredPosition.y);
        locationUI.rectTransform.anchoredPosition = startPos;

        if(GameManager.Stone.growingStone.stoneStat.StoneType.Equals(STONE_TYPE.LimeStone))
            stoneControllerScript.life = 3;
        else if(GameManager.Stone.growingStone.stoneStat.StoneType.Equals(STONE_TYPE.Granite))
            stoneControllerScript.life = 5;

        InitializeLifeImages(stoneControllerScript.life);
        oldLife = stoneControllerScript.life;
        
        gameTimer = 0;
    }

    private void InitializeLifeImages(int lifeCount)
    {
        stoneLifeImages = new RawImage[lifeCount];
        for (int i = 0; i < lifeCount; i++)
        {
            stoneLifeImages[i] = Instantiate(stoneLifeImage);
            stoneLifeImages[i].transform.SetParent(lifeTransform.transform);
            stoneLifeImages[i].rectTransform.localScale = new Vector3(2.1049f, 2.1049f, 2.1049f);
            stoneLifeImages[i].rectTransform.localPosition = new Vector3(-238 + (210 * i), 1193, 0);
        }
    }

    private void StartGame()
    {
        SetData();
        startPanel.SetActive(false);
    }

    private void RestartGame()
    {
        SetData();
        isGameOver = false;
        endPanel.SetActive(false);
    }

    private void EndGame(bool isCleared)
    {
        isGameOver = true;
        if (isCleared)
        {
            clearPanel.SetActive(true);
            ITEM_TYPE randomItem = (ITEM_TYPE)Random.Range(0, System.Enum.GetValues(typeof(ITEM_TYPE)).Length);
            Debug.Log(randomItem);
            GameManager.Item.AcquireItem(randomItem);
        }
        else
        {
            endPanel.SetActive(true);
        }
        minigameControl = false;
    }

    private void GotoSauna()
    {
        GameManager.Instance.ChangeMap(MAP_TYPE.Sauna);
    }
}