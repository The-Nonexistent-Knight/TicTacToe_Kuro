using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoveRecord
{
    public int x;
    public int y;
    public string previousSymbol;
    public Player player;

    public MoveRecord(int x, int y, string symbol, Player player)
    {
        this.x = x;
        this.y = y;
        this.previousSymbol = symbol;
        this.player = player;
    }
}
