using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAIStrategy
{
    (int x, int y) GetMove(Cell[,] grid);
}
