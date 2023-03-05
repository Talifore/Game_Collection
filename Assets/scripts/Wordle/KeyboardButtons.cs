using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button key = GetComponent<Button>();
        key.onClick.AddListener(clicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void clicked()
    {
        Text child = GetComponentInChildren<Text>();
        string letter = child.text;
        WordleManager.Wordle.ClickedChar(letter);
    }
}
