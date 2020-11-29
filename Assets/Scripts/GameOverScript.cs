using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : BaseScript
{
    private bool showGameOver = false;
    private int x = (Screen.width - 260) / 2;
    private int y = (Screen.height - 260) / 2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject maybePlayer = CheckCloseToTag("Player", 10);
        showGameOver = maybePlayer != null;
    }

    void OnGUI()
    {
        if (showGameOver)
            GUI.Box(new Rect(x, y + (30 / 2), 260, 30), "Рівень пройдено");
    }
}
