using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Mechanics")]
    public CratePrefab[] cratePrefabs;
    public float intialTime;
    public float bonusTime;
    public int score;
    public float xRange;
    public float zRange;

    private int waveNo = 1;
    private float timer=0;
    private GameManagerUi gameManagerUi;
    
    public bool isGameOver=false;
    public bool isGamePaused = false;

    private void Start() {
        Time.timeScale = 1;
        gameManagerUi = GameObject.FindObjectOfType<GameManagerUi>().GetComponent<GameManagerUi>();
        timer= intialTime;
        AddScore(0);
    }

    private void Update() {
        timer -= Time.deltaTime;
        gameManagerUi.timerText.text = Mathf.FloorToInt(timer).ToString();

        if (timer <0)
        {
            isGameOver = true;
            gameManagerUi.GameOver();
        }

        int count = FindObjectsOfType<LootCrate>().Length;
        if(count<=0)
        {
            foreach(var obj in GameObject.FindGameObjectsWithTag("Crate")){
                obj.gameObject.SetActive(false);
            }

            timer += bonusTime;
            SpawnCrates(waveNo);
            waveNo++;
        }

        if(Input.GetKeyDown(KeyCode.Space) && !isGameOver && !isGamePaused)
            gameManagerUi.Pause();

    }

    void SpawnCrates(int wave)
    {
        foreach(CratePrefab cratePrefab in cratePrefabs)
        {
            SpawnObject(cratePrefab.prefab,Mathf.FloorToInt(Random.Range(cratePrefab.min,cratePrefab.Max+wave)));
        }
    }

    void SpawnObject(GameObject prefab,int n)
    {
        for(int i=0;i<n;i++){
            GameObject obj= ObjectPooler.SharedInstance.GetPooledObject(prefab.name);
            obj.SetActive(true);
            obj.transform.position = GenerateRandomPosition();
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

    [System.Serializable]
    public class CratePrefab
    {
        public GameObject prefab;
        public float min;
        public float Max;
    }
}
