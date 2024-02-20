using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public static UnityAction ActionGameStart, ActionLevelPassed , ActionLevelLosed;

    public GameData gameData;

    public GameObject[] levels;
    private void OnEnable()
    {
        gameData = SaveSystem.Load();
        LoadCurrentLevel();
        
    }
    public override void  Awake()
    {
        
        base.Awake();
       
       
    }
    
    public void LoadCurrentLevel()
    {

        Instantiate(levels[gameData.playerLevel], levels[gameData.playerLevel].transform.position, levels[gameData.playerLevel].transform.rotation);
      
    }
    public void LoadNextLevel()
    {
        //For debbuging 0
        if(gameData.playerLevel == gameData.maxLevel)
        {
            gameData.playerLevel = 0;
            SaveSystem.Save(gameData);
            SceneManager.LoadScene(gameData.playerLevel);
        }
        else
        {
            gameData.playerLevel++;
            SaveSystem.Save(gameData);
            SceneManager.LoadScene(gameData.playerLevel);

        }
       
    }

   
}
