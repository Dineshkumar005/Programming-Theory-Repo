using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Mechanics")]
    public GameObject[] cratePrefabs;
    public float intialTime;
    public float bonusTime;
    public int score;
    public float xRange;
    public float zRange;

    private int waveNo = 1;
    private float timer=0;
    private float maxTime;
    private GameManagerUi gameManagerUi;
    
    public static bool isGameOver=false;
    public static bool isGamePaused = false;

    private void Start() {
        gameManagerUi = GameObject.FindObjectOfType<GameManagerUi>().GetComponent<GameManagerUi>();
        maxTime = intialTime;
        AddScore(0);
        SpawnCrates(0);
    }

    private void Update() {
        timer += Time.deltaTime;
        gameManagerUi.timerText.text = Mathf.FloorToInt(timer).ToString();

        if (timer >= maxTime)
        {
            isGameOver = true;
            gameManagerUi.GameOver();
        }

        int count = FindObjectsOfType<LootCrate>().Length;
        if(count<=0)
        {
            foreach(var obj in GameObject.FindGameObjectsWithTag("Crate")){
                Destroy(obj);
            }

            maxTime += bonusTime;
            SpawnCrates(waveNo);
            waveNo++;
        }

        if(Input.GetKeyDown(KeyCode.Space) && !isGameOver && !isGamePaused)
            gameManagerUi.Pause();

    }

    void SpawnCrates(int wave)
    {
        SpawnObject(cratePrefabs[0],Random.Range(15,25+wave));
        SpawnObject(cratePrefabs[1],Random.Range(20,25+wave));
        SpawnObject(cratePrefabs[2], Random.Range(2, 4 + wave));
        SpawnObject(cratePrefabs[3], Random.Range(3, 7 + wave));
    }

    void SpawnObject(GameObject prefab,int n)
    {
        for(int i=0;i<n;i++){
            Instantiate(prefab, GenerateRandomPosition(),Random.rotation);
        }
    }

    Vector3 GenerateRandomPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), 1, Random.Range(-zRange, zRange));
    }

    public void AddScore(int value)
    {
        score += value;
        gameManagerUi.scoreText.text = "Score : " + score;
    }
}
