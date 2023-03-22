using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TicTacToeManager : MonoBehaviour
{
    private string[] board = new string[9];
    public Button[] TrueBoard;
    private bool turn;
    private string playerChar;
    private string AIchar;
    private bool game;
    private string winChar;

    // Start is called before the first frame update
    void Start()
    {
        game = true;
        for(int i = 0; i < 9; i++)
        {
            board[i] = "n";
        }
        bool n = randomBoolean();
        if (n)
        {
            turn = true;
            playerChar = "X";
            AIchar = "O";
        }
        else
        {
            turn = false;
            playerChar = "O";
            AIchar = "X";
            aiTurn();
        }
        TrueBoard[0].onClick.AddListener(() => playerTurn(1));
        TrueBoard[1].onClick.AddListener(() => playerTurn(2));
        TrueBoard[2].onClick.AddListener(() => playerTurn(3));
        TrueBoard[3].onClick.AddListener(() => playerTurn(4));
        TrueBoard[4].onClick.AddListener(() => playerTurn(5));
        TrueBoard[5].onClick.AddListener(() => playerTurn(6));
        TrueBoard[6].onClick.AddListener(() => playerTurn(7));
        TrueBoard[7].onClick.AddListener(() => playerTurn(8));
        TrueBoard[8].onClick.AddListener(() => playerTurn(9));
    }

    public bool randomBoolean()
    {
        if (Random.value >= 0.5)
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void playerTurn(int i)
    {
        if (game)
        {
            if (turn)
            {
                board[i - 1] = playerChar;
                TrueBoard[i - 1].transform.GetChild(0).GetComponent<Text>().text = playerChar;
                turn = !turn;
            }
            Debug.Log(result());
            aiTurn();
        }
    }

    public void aiTurn()
    {
        bool placed = false;
        if (game)
        {
            if (turn == false)
            {
                bool test = true;
                int n = 0;
                while (test)
                {
                    int p = Random.Range(1, 9);
                    if (board[p - 1].Equals("n"))
                    {
                        board[p - 1] = AIchar;
                        TrueBoard[p - 1].transform.GetChild(0).GetComponent<Text>().text = AIchar;
                        turn = !turn;
                        test = false;
                        placed = true;
                    }
                    if (n == 10)
                    {
                        test = false;
                    }
                }
                for (int i = 0; i < 9; i++)
                {
                    if (board[i].Equals("n") && placed == false)
                    {
                        board[i] = AIchar;
                        TrueBoard[i].transform.GetChild(0).GetComponent<Text>().text = AIchar;
                        placed = true;
                    }
                }
            }
            Debug.Log(result());
        }
    }

    public bool result()
    {
        // Top Row
        if (board[0].Equals(board[1]) && board[1].Equals(board[2]))
        {
            if (!board[0].Equals("n"))
            {
                game = false;
                winChar = board[0];
            }
        }
        // Middle Row
        if (board[3].Equals(board[4]) && board[4].Equals(board[5]))
        {
            if (!board[3].Equals("n"))
            {
                game = false;
                winChar = board[3];
            }
        }
        // Bottom Row
        if (board[6].Equals(board[7]) && board[7].Equals(board[8]))
        {
            if (!board[6].Equals("n"))
            {
                game = false;
                winChar = board[6];
            }
        }
        // Left column
        if (board[0].Equals(board[3]) && board[3].Equals(board[6]))
        {
            if (!board[0].Equals("n"))
            {
                game = false;
                winChar = board[0];
            }
        }
        // Middle column
        if (board[1].Equals(board[4]) && board[4].Equals(board[7]))
        {
            if (!board[1].Equals("n"))
            {
                game = false;
                winChar = board[1];
            }
        }
        // Right column
        if (board[2].Equals(board[5]) && board[5].Equals(board[8]))
        {
            if (!board[2].Equals("n"))
            {
                game = false;
                winChar = board[2];
            }
        }
        // Diagonal 1
        if (board[0].Equals(board[4]) && board[4].Equals(board[8]))
        {
            if (!board[0].Equals("n"))
            {
                game = false;
                winChar = board[0];
            }
        }
        // Diagonal 2
        if (board[6].Equals(board[4]) && board[4].Equals(board[2]))
        {
            if (!board[6].Equals("n"))
            {
                game = false;
                winChar = board[6];
            }
        }

        int n = 0;
        for (int i = 0; i < 9; i++)
        {
            if (board[i].Equals("n"))
            {
                n++;
            }
        }

        if (n == 0)
        {
            game = false;
            winChar = "n";
        }

        if (game == false)
        {
            if (winChar.ToUpper() == "X")
            {
                Debug.Log("X wins");
            }
            else if (winChar.ToUpper() == "O")
            {
                Debug.Log("O wins");
            }
            else
            {
                Debug.Log("Draw");
            }
        }
        return game;
    }
}
