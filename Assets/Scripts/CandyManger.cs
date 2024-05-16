using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyManger : MonoBehaviour
{
    const int DefaultCandyAmount = 30;
    const int Recoveryseconds = 10;

    //現在のキャンディのストック数
    public int candy = DefaultCandyAmount;
    // ストック回数までの残り秒数
    int counter;

    public void ConsumeCandy()
    {
        if (candy > 0) candy--;
    }

    public int GetCandyAmount()
    {
        return candy;
    }
    public void AddCandy(int amount)
    {
        candy += amount;
    }

    void OnGUI()
    {
        GUI.color = Color.black;
        //キャンディのストック表示
        string label = "Candy : " + candy;
        //回復カウントしている時だけ秒数を表示
        if (counter > 0) label = label + "(" + counter + "s)";
        GUI.Label(new Rect(50, 50, 100, 30), label);
    }

    void Update()
    {
        //キャンディのストックがデフォルトより少なく、
        //回復カウントをしていないときにカウントをスタートさせる
        if (candy < DefaultCandyAmount && counter <= 0)
        {
            StartCoroutine(RecoverCandy());
        }
    }

    IEnumerator RecoverCandy()
    {
        counter = Recoveryseconds;
        //1秒ずつカウントを進める
        while (counter > 0)
        {
            yield return new WaitForSeconds(1.0f);
            counter--;
        }

        candy++;
    }



}
