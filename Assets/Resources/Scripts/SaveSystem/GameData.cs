using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
   
    public int playerLevel;
    public int maxLevel;
    public bool soundON;
   
    public GameData()
    {
        playerLevel = 0;
        maxLevel = 0;
        soundON = true;
    
    }
}
