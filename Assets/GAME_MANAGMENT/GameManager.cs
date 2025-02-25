using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Ursaanimation.CubicFarmAnimals;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerGold;
    public GameObject GoldDisplay;

    public List<SheepLogic> sheepList = new List<SheepLogic>();
    public List<WolfLogic> wolfList = new List<WolfLogic>();

    public GameObject[] sheepPrefabs; 
    public GameObject wolfPrefab;
    public Transform[] wolfSpawnPoints;

    public int startingSheepCount = 5;
    public Transform[] sheepSpawnPoints; 

    public int nightCount = 0;
    private bool isNight = false;

    public GameObject GameDataCanva;
    public GameObject NightDataCanva;

    public GameObject Sun;
    public GameObject Moon;
    public GameObject Stars;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        SpawnSheep(startingSheepCount);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Sun.SetActive(true);
        Moon.SetActive(false);
        Stars.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R) && !isNight)
        {
            StartNight();
        }

        GameDataCanva.GetComponent<TMP_Text>().text = "day " + nightCount + " - " + sheepList.Count + " sheeps remaining";
        GoldDisplay.GetComponent<TMP_Text>().text = playerGold + " golds";

        if (isNight)
        {
            NightDataCanva.GetComponent<TMP_Text>().text = wolfList.Count + " wolfs remaining";
        }
        else
        {
            NightDataCanva.GetComponent<TMP_Text>().text = "Press r to start next night";
        }
    }

    private void SpawnSheep(int SheepNumber)
    {
        for (int i = 0; i < SheepNumber; i++)
        {
            Vector3 spawnPos = sheepSpawnPoints[Random.Range(0, sheepSpawnPoints.Length)].position;
            GameObject randomSheepPrefab = sheepPrefabs[Random.Range(0, sheepPrefabs.Length)];

            GameObject newSheep = Instantiate(randomSheepPrefab, spawnPos, Quaternion.identity);
            SheepLogic sheep = newSheep.GetComponent<SheepLogic>();
            sheep.CurrentAge = 0;
            sheepList.Add(sheep);
        }
    }

    public void StartNight()
    {
        if (isNight) return;

        Sun.SetActive(false);
        Moon.SetActive(true);
        Stars.SetActive(true);

        isNight = true;
        nightCount++;

        for (int i = 0; i < nightCount*2; i++)
        {
            Vector3 spawnPos = wolfSpawnPoints[Random.Range(0, wolfSpawnPoints.Length)].position;
            GameObject randomSheepPrefab = sheepPrefabs[Random.Range(0, sheepPrefabs.Length)];

            GameObject newWolf = Instantiate(wolfPrefab, spawnPos, Quaternion.identity);
            WolfLogic wolf = newWolf.GetComponent<WolfLogic>();
            wolfList.Add(wolf);
        }
    }

    public void WolfDied(WolfLogic wolf)
    {
        wolfList.Remove(wolf);
        playerGold += 100;
        if (wolfList.Count == 0) EndNight();
    }

    private void EndNight()
    {
        Sun.SetActive(true);
        Moon.SetActive(false);
        Stars.SetActive(false);
        isNight = false;

        foreach (var sheep in sheepList)
        {
            sheep.CurrentAge++;
        }

        SpawnSheep((int)(sheepList.Count / 2));
    }


    public void SheepDied(SheepLogic sheep)
    {
        sheepList.Remove(sheep);
        if (sheepList.Count == 0) GameOver();
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER !");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnNightButtonPressed()
    {
        StartNight();
    }

}
