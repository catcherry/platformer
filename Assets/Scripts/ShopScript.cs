using System;
using System.Collections.Generic;
using UnityEngine;


public class ShopScript : BaseScript
{
    private bool showWindow = false;
    private String customError = "";
    private bool showErrorWindow = false;
    private bool showSuccessWindow = false;
    private bool showCustomError = false;
    private float time;
    private float period = 2f;
    private Dictionary<string, Delegate> dict;
    private PlayerScript player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

        dict = new Dictionary<string, Delegate>();
        dict.Add("Купити 1 життя за 50 монет", new Func<bool>(BuyLive));
        dict.Add("Купити 2 життя за 100 монет", new Func<bool>(BuyTwoLives));
        dict.Add("Вийти", new Func<bool>(OnExit));
    }

    void Update()
    {
        if ((showErrorWindow || showCustomError || showSuccessWindow) && (Time.time > time + period))
        {
            showSuccessWindow = false;
            showErrorWindow = false;
            showWindow = true;
            showCustomError = false;
            customError = "";
        }
    }

    void OnGUI()
    {
        int windowWidth = 275;
        int windowHeight = (dict.Count * 21)+25;
        int x = (Screen.width - windowWidth) / 2;
        int y = (Screen.height - windowWidth) / 2;

        if (showErrorWindow)
        {
            GUI.Box(new Rect(x, y + (windowHeight/2), 260, 30), "Недостатньо грошей на рахунку");
            return;
        }

        if (showCustomError)
        {
            GUI.Box(new Rect(x, y + (windowHeight/2), 260, 30), customError);
            return;
        }

        if (showSuccessWindow)
        {
            GUI.Box(new Rect(x, y + (windowHeight / 2), 260, 30), "Ви здійснили покупку");
            return;
        }

        if (showErrorWindow || showSuccessWindow || !showWindow) return;
        GUI.Window(
            0,
            new Rect(x, y, windowWidth, windowHeight),
            DoWindow,
            "Аптека"
        );
    }

    bool OnExit()
    {
        showWindow = false;
        showSuccessWindow = false;
        showErrorWindow = false;
        customError = "";
        return true;
    }

    void ShowSuccWindow()
    {
        time = Time.time;
        showErrorWindow = false;
        showWindow = false;
        customError = "";
        showSuccessWindow = true;
    }

    void ShowLowMoneyError()
    {
        time = Time.time;
        showErrorWindow = true;
        customError = "";
        showWindow = false;
        showSuccessWindow = false;
    }

    void ShowCustomError(String error)
    {
        time = Time.time;
        showErrorWindow = false;
        customError = error;
        showCustomError = true;
        showWindow = false;
        showSuccessWindow = false;
    }

    bool tryToBuy(int price)
    {
        bool canBuy = player.GetCoins() >= price;
        if (!canBuy)
        {
            ShowLowMoneyError();
            return false;
        }
        player.RemoveCoins(price);
        ShowSuccWindow();
        return true;
    }

    bool BuyTwoLives()
    {
        int price = 100;
        if (player.GetLives() > 1)
        {
            ShowCustomError("Вам нічого не потрібно");
            return false;
        }
        else if (!tryToBuy(price))
        {
            return false;
        }
        player.AddLives(2);
        player.UpdateUI();
        return true;
    }


    bool BuyLive()
    {
        int price = 500;
        if (player.GetLives() >= 3) {
            ShowCustomError("Вам нічого не потрібно");
            return false;
        }
        else if (!tryToBuy(price)) {
            return false;
        }
        player.AddLives(1);
        player.UpdateUI();
        return true;
    }

    void DoWindow(int windowID)
    {
        int i = 1;
        foreach (KeyValuePair<string, Delegate> pair in dict)
        {  
            if (GUI.Button(new Rect(50, i * 21, 200, 20), pair.Key))
            {
                pair.Value.DynamicInvoke();
            }
            i += 1;
        }
    }

    public void Show()
    {
        showWindow = true;
    }
}
