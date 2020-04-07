using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_Trump : Puzzle
{
    public Button winButton;
    public Button loseButton;
    public override void InitPuzzle()
    {
        winButton.onClick.AddListener(() =>
        {
            DisablePuzzle();
//            ChatSystem2.instance.StartChat("Dialog", "카드놀이_승리", () => { });
        });

        loseButton.onClick.AddListener(() =>
        {
            DisablePuzzle();
//            ChatSystem2.instance.StartChat("Dialog", "카드놀이_패배", () => { });
            SpeechBaloonManager.instance.AddBaloon(3);
        });
    }
}
