using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{
    public abstract void StartPuzzle(PuzzleNode from);
    public abstract void OnExit(PuzzleNode from);
}
