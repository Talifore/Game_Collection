using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Blackjack()
    {
        SceneManager.LoadScene("Blackjack");
    }

    public void Wordle()
    {
        SceneManager.LoadScene("Wordle");
    }

    public void TicTacToe()
    {
        SceneManager.LoadScene("TicTacToe");
    }

    public void Pong()
    {
        SceneManager.LoadScene("Pong");
    }

    public void SpaceInvader()
    {
        SceneManager.LoadScene("SpaceInvader");
    }
}
