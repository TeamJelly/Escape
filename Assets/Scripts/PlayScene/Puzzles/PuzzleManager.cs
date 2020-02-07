using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    Dictionary<string, Puzzle> puzzleFinder = new Dictionary<string, Puzzle>();

    public static PuzzleManager instance;

    private void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        GameObject[] puzzles = GameObject.FindGameObjectsWithTag("Puzzle");

        foreach(GameObject g in puzzles)
        {
            Puzzle p = g.GetComponent<Puzzle>();
            puzzleFinder.Add(p.puzzleName, p);
        }
    }
    public Puzzle GetPuzzleWithName(string name)
    {
        return puzzleFinder[name];
    }
    public void StartPuzzleWithName(string name)
    {
        if (!puzzleFinder.ContainsKey(name)) return;
        Puzzle p = GetPuzzleWithName(name);
        p.EnablePuzzle();
    }

}
