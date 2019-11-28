using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleExample : Puzzle
{
    public GameObject puzzlePanel;
    public GameObject wall;
    public override void StartPuzzle(PuzzleNode from)
    {
        puzzlePanel.SetActive(true);
        Button button = puzzlePanel.GetComponentInChildren<Button>();
        button.gameObject.GetComponentInChildren<Text>().text = "퍼즐 풀기";
        button.onClick.AddListener(()=>OnExit(from));
    }
    public override void OnExit(PuzzleNode from)
    {
        from.isSolved = true;
        from.beforeNode.HideNodes();
        from.beforeNode.nodes.Remove(from);
        foreach(WorkNode w in from.nodes)
        {
            from.beforeNode.nodes.Add(w);
            w.button.onClick.RemoveAllListeners();
            w.button.onClick.AddListener(() => w.OnClick(from.beforeNode));
        }
        puzzlePanel.SetActive(false);
        wall.SetActive(false);
        from.beforeNode.OnClick(from.beforeNode);
    }
}
