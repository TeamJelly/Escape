using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleNode : WorkNode
{
    Puzzle puzzle;
    public bool isSolved = false;
    private void Awake()
    {
        puzzle = GetComponent<Puzzle>();
    }
    public override void OnClick(WorkNode from)
    {
        if (isSolved) puzzle.OnExit(this);
        else
        {
            puzzle.StartPuzzle(this);
        }
        
    }
}
