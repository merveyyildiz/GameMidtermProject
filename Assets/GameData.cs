using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData 
{
    public int score;
    public float posy;
    public float posx;


    public GameData(Player player)
    {
        score = Player.score;
        posy = Player.y; 
        posx = Player.x;
      
    }
}
