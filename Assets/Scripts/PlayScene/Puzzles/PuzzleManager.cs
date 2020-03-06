using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    Dictionary<string, Puzzle> puzzleFinder = new Dictionary<string, Puzzle>();

    public static PuzzleManager instance;
    Puzzle currentPuzzle;

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
    public void StartPuzzleWithName(string name)//아이템 클릭시 퍼즐 실행 -> 해당 아이템 이름으로 퍼즐이름지정
    {
        if (!puzzleFinder.ContainsKey(name)) return;
        Puzzle p = GetPuzzleWithName(name);
        if (currentPuzzle == p) return;
        if (currentPuzzle != null) currentPuzzle.thisUI.gameObject.SetActive(false);
        currentPuzzle = p;
        PuzzleDatabase.SetPuzzleState(name, 1);
        p.EnablePuzzle();
    }
    
}
