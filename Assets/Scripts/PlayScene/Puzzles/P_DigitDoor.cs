using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_DigitDoor : Puzzle
{
    public Button submitButton;
    public Button resetPasswdButton;


    public Text hint;
    public GameObject resetPage;
    public GameObject unlockMessage;
    public GameObject errorMessage;
    public InputField inputPasswd;
    public InputField inputHint1;
    public InputField inputHint2;
    public InputField inputHint3;

    string passwd = "한글로 Hello World!";
    public override void InitPuzzle()
    {
        if (PuzzleDatabase.GetPuzzleState(PuzzleName) == 3)
        {
            hint.text = "힌트 : 초기화 비밀번호";
            passwd = "0000";
        }
        /*OnEnd.AddListener(()=>
        {
            PuzzleDatabase.SetPuzzleState(PuzzleName, 2);
            DataManager.Save_Auto();
            OnDisable.Invoke();
        });
        OnDisable.AddListener(() =>
        {
            unlockMessage.SetActive(true);
        });*/

        submitButton.onClick.AddListener(Check_Input);
        resetPasswdButton.onClick.AddListener(Check_Hint);
    }

    public new void EnablePuzzle()
    {
        inputPasswd.text = "";
        inputHint1.text = "";
        inputHint2.text = "";
        inputHint3.text = "";
        resetPage.SetActive(false);
        errorMessage.SetActive(false);
//        base.EnablePuzzle();
    }

    void Check_Input()
    {
        if (inputPasswd.text == "*#*#4636#*#*")
        {
            resetPage.SetActive(true);
        }
        else if (inputPasswd.text == passwd)
        {
            //OnEnd.Invoke();
        }
        else
        {
            errorMessage.SetActive(true);
            BackgroundManager.instance.AddFailureCount();
        }
    }

    void Check_Hint()
    {
        if (inputHint1.text == "범죄왕" &&
            inputHint2.text == "카레" &&
            inputHint3.text == "오빠")
        {
            PuzzleDatabase.SetPuzzleState(PuzzleName, 3);
            DataManager.Save_Auto();
            hint.text = "힌트 : 초기화 비밀번호";
            passwd = "0000";
            resetPage.SetActive(false);
        }
        else
        {
            errorMessage.SetActive(true);
            BackgroundManager.instance.AddFailureCount();
        }
    }
}
