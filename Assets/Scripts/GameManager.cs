using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public List<GameObject> Words;
    private int count = 0, tipsCount = 2;
    public string word, currentWord;
    public Text inputWord, tipsCountText;
    public GameObject alertPanel, tipsButton;
    public Text alertPanelText;
    // Start is called before the first frame update
    public static GameManager instance;
    void Start()
    {
        instance = this;
        word = WordSelector.GetAWord().ToUpper();
        tipsCountText.text = "Tips: " + tipsCount.ToString();
        initWordObjects();
    }

    private void initWordObjects()
    {
        foreach (var w in Words)
        {
            for (int i = 0; i < 5; i++)
            {
                LetterController l = w.transform.GetChild(i).GetComponent<LetterController>();
                l.Init(word[i]);
            }
        }
    }

    // Update is called once per frame
    public void submitNewWord()
    {
        if (inputWord.text.Length == 5)
        {
            currentWord = inputWord.text;
            currentWord = currentWord.ToUpper();
            for (int i = 0; i < 5; i++)
            {
                LetterController l = Words[count].transform.GetChild(i).GetComponent<LetterController>();
                l.setLetter(currentWord[i]);
            }
            count++;
            if (currentWord == word)
            {
                showSuccess();
            }
            if (count == 6 && currentWord != word)
            {
                showFailed();
            }
            inputWord.text = "";
        }

    }
    public void submitTipsWord()
    {
        currentWord = currentWord.ToUpper();
        for (int i = 0; i < 5; i++)
        {
            LetterController l = Words[count].transform.GetChild(i).GetComponent<LetterController>();
            l.setLetter(currentWord[i]);
        }
        count++;
        if (currentWord == word)
        {
            showSuccess();
        }
        if (count == 6 && currentWord != word)
        {
            showFailed();
        }
        inputWord.text = "";
        StartCoroutine(animate(Words[count - 1]));
    }

    IEnumerator animate(GameObject gb)
    {
        gb.transform.localScale = new Vector3(1.05f, 1.05f, 1f);
        yield return new WaitForSeconds(0.5f);
        gb.transform.localScale = new Vector3(1f, 1f, 1f);
    }


    public void showSuccess()
    {
        alertPanel.SetActive(true);
        alertPanelText.text = "Congratulations!\nYou Have Done It!";
    }

    public void showFailed()
    {
        alertPanel.SetActive(true);
        alertPanelText.text = "Oops! Try Again";
    }

    public void restart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void showTips()
    {
        if (currentWord == "")
        {
            return;
            currentWord = CreateString(5);
            while (currentWord == word)
            {
                currentWord = CreateString(5);
            }
        }
        int indx = Random.Range(0, word.Length);
        while (currentWord[indx] == word[indx])
        {
            indx = Random.Range(0, word.Length);
        }
        currentWord = currentWord.Remove(indx, 1);
        currentWord = currentWord.Insert(indx, word[indx].ToString());
        submitTipsWord();
        tipsCount--;
        tipsCountText.text = "Tips: " + tipsCount.ToString();
        if (tipsCount == 0)
        {
            tipsButton.SetActive(false);
        }
    }
    private string CreateString(int stringLength)
    {
        const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
        char[] chars = new char[stringLength];

        for (int i = 0; i < stringLength; i++)
        {
            chars[i] = allowedChars[Random.Range(0, allowedChars.Length)];
        }

        return new string(chars);
    }
}
