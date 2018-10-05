using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameDifficulty
{
    EASY,
    MEDIUM,
    HARD
}

[CreateAssetMenu(fileName = "NewWorldState", menuName = "World/WorldState", order = 1)]
public class WorldStateSO : ScriptableObject
{
    public int x;
    public int y;
    public int numberOfPirates;

    
    GameDifficulty gameDifficulty = GameDifficulty.MEDIUM;

    public WorldStateSO(int x, int y, int numberOfPirates)
    {
        this.x = x;
        this.y = y;
        this.numberOfPirates = numberOfPirates;
    }
	
    public void Initialize(int x, int y, int numberOfPirates)
    {
        this.x = x;
        this.y = y;
        this.numberOfPirates = numberOfPirates;
    }
}
