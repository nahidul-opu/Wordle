using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LetterController : MonoBehaviour
{
    public Image bg;
    public Text letter;
    public string CorrectLetter, CurrentLetter;
    // Start is called before the first frame update
    public void Init(char l)
    {
        CorrectLetter = l.ToString();
    }

    private int countOccurrance(string s)
    {
        int count = 0;
        foreach (char c in s)
            if (c.ToString() == CurrentLetter) count++;
        return count;
    }
    public void setLetter(char l)
    {
        CurrentLetter = l.ToString();
        letter.text = CurrentLetter;
        int cntOriginal = countOccurrance(GameManager.instance.word);
        int cntCurr = countOccurrance(GameManager.instance.currentWord);
        if (CurrentLetter == CorrectLetter)
        {
            bg.color = Color.green;
        }
        else if (GameManager.instance.word.Contains(CurrentLetter) && cntOriginal == cntCurr)
        {
            bg.color = Color.yellow;
        }
        else
        {
            bg.color = Color.white;
        }
    }
}
