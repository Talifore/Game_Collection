using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WordleManager : MonoBehaviour
{
    public string correctWord;

    public static WordleManager Wordle;

    private List<string> wordList = new List<string>();

    private List<string> Dictionary = new List<string>();

    public List<Transform> wordBoxes = new List<Transform>();

    private int currentBox;

    private int currentRow;

    private int ROWCOUNT = 5;

    private int TOTALROWS = 6;

    private Color colorCorrect = new Color(0.3254902f, 0.5529412f, 0.3058824f);

    private Color colorIncorrectPlace = new Color(0.7098039f, 0.6235294f, 0.2313726f);
    
    private Color colorUnused = new Color(0.2039216f, 0.2039216f, 0.2f);

    public Sprite clearedWordBoxSprite;

    // Start is called before the first frame update
    void Start()
    {
        if (Wordle == null)
        {
            Wordle = this;
        }
        else
        {
            Destroy(this);
        }

        AddWordsToList(Application.streamingAssetsPath + "/answers.txt", wordList);

        AddWordsToList(Application.streamingAssetsPath + "/allowed.txt", Dictionary);

        correctWord = GetRandomWord();

        Debug.Log(correctWord);

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LetterToBox(string letter)
    {
        if (currentRow > TOTALROWS)
        {
            Debug.Log("No More Rows");
            return;
        }

        if (wordBoxes[(currentRow * ROWCOUNT) + currentBox].GetChild(0).GetComponent<Text>().text == "")
        {
            wordBoxes[(currentRow * ROWCOUNT) + currentBox].GetChild(0).GetComponent<Text>().text = letter;
        }
        if ((currentRow * ROWCOUNT) + currentBox < (currentRow * ROWCOUNT) + 4)
        {
            currentBox++;
        }
    }

    private string GetRandomWord()
    {
        string randomWord = wordList[UnityEngine.Random.Range(0, wordList.Count)];
        Debug.Log(randomWord);
        return randomWord.ToLower();
    }

    private void AddWordsToList(string v, List<string> list)
    {
        StreamReader reader = new StreamReader(v);
        string text = reader.ReadToEnd();

        Debug.Log(text);

        char[] separator = { '\n' };
        string[] Words = text.Split(separator);

        foreach (string word in Words)
        {
            list.Add(word); 
        }

        reader.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickedChar(string letter)
    {
        Debug.Log(letter);
        LetterToBox(letter);
    }

    public void LetterFromBox()
    {
        if (currentRow > TOTALROWS)
        {
            Debug.Log("No More Rows");
            return;
        }

        int currentlySelected = (currentRow * ROWCOUNT) + currentBox;

        if (wordBoxes[currentlySelected].GetChild(0).GetComponent<Text>().text == "")
        {
            if (currentlySelected > ((currentRow * ROWCOUNT)))
            {
                currentBox--;
            }
            currentlySelected = (currentRow * ROWCOUNT) + currentBox;
        }
        wordBoxes[currentlySelected].GetChild(0).GetComponent<Text>().text = "";
    }

    public void EnterKey()
    {
        string guess = "";
        for (int n = (currentRow * ROWCOUNT); n < (currentRow * ROWCOUNT) + currentBox + 1; n++)
        {
            guess += wordBoxes[n].GetChild(0).GetComponent<Text>().text;
        }
        guess = guess.ToLower();

        Debug.Log("Player Guess: " + guess);
        if (guess == correctWord)
        {
            Debug.Log("Correct word!");
        }
        else if(!Dictionary.Contains(guess))
        {
            Debug.Log("Not a word.");
            for(int i = 0; i < 5; i++)
            {
                LetterFromBox();
            }
            return;
        }
        else
        {
            Debug.Log("Wrong word.");
            currentBox = 0;
            currentRow++;
        }
        if (currentRow > TOTALROWS)
        {
            Debug.Log("No More Rows");
            return;
        }
        CheckWord(guess);
    }


    void CheckWord(string guess)
    {
        char[] playerGuessArray = guess.ToCharArray();
        //string tempPlayerGuess;
        char[] correctWordArray = correctWord.ToCharArray();
        string tempCorrectWord;

        for (int i = 0; i < 5; i++)
        {
            if (playerGuessArray[i] == correctWordArray[i])
            {
                playerGuessArray[i] = '0';
                correctWordArray[i] = '0';
            }
        }

        //tempPlayerGuess = "";
        tempCorrectWord = "";
        for (int i = 0; i < 5; i++)
        {
            //tempPlayerGuess += playerGuessArray[i];
            tempCorrectWord += correctWordArray[i];
        }

        for (int i = 0; i < 5; i++)
        {
            if (tempCorrectWord.Contains(playerGuessArray[i].ToString()) && playerGuessArray[i] != '0')
            {
                char playerCharacter = playerGuessArray[i];
                playerGuessArray[i] = '1';

                int index = tempCorrectWord.IndexOf(playerCharacter, 0);
                correctWordArray[index] = '.';
                tempCorrectWord = "";
                for (int j = 0; j < 5; j++)
                {
                    tempCorrectWord += correctWordArray[j];
                }
            }
        }
        

        for (int i = 0; i < 5; i++)
        {
            Debug.Log(playerGuessArray[i]);
            Debug.Log(playerGuessArray[i].Equals('0'));
            Debug.Log(playerGuessArray[i].Equals('1'));
            if (playerGuessArray[i].Equals('0'))
            {
                wordBoxes[i].GetComponent<Image>().sprite = clearedWordBoxSprite;
                wordBoxes[i].GetComponent<Image>().color = colorCorrect;
                Debug.Log("Green");
            }
            else if(playerGuessArray[i].Equals('1'))
            {
                wordBoxes[i].GetComponent<Image>().sprite = clearedWordBoxSprite;
                wordBoxes[i].GetComponent<Image>().color = colorIncorrectPlace;
                Debug.Log("Yellow");
            }
            else
            {
                wordBoxes[i].GetComponent<Image>().sprite = clearedWordBoxSprite;
                wordBoxes[i].GetComponent<Image>().color = colorUnused;
                Debug.Log("Gray");
            }
        }
    }

}
