using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushers : MonoBehaviour
{

    Vector3 startPosition;
    // Vector3 startPosition { get; set; }
    //移動量プロパティ
    public float amplitude;
    //移動速度プロパティ
    public float speed;

    void Start()
    {
        // Debug.Log(startPosition.x);
        startPosition = transform.localPosition;

        //仮の変数にコピーするやり方
        // Vector3 pos = transform.position;
        // pos.y = 100;
        // transform.position = pos;

        //新しいVector3の構造体
        //c#では変数宣言のとき、右辺の型が明確なときはvarを使って省略していいルールがある
        //後から型変更はできない。あくまで労力削減のため
        var pos = new Vector3(100, transform.position.y, transform.position.x);
        transform.position = pos;


    }

    void Update()
    {
        //変位を計算
        /*
        時間の流れに進み、三角関数のサインカーブの数値を与える(-1~1)
        循環した流れのなみのうごき
        Time.timeはゲームを始めた時間
        */
        float z = amplitude * Mathf.Sin(Time.time * speed);
        //zを変位させたポジションに再設定
        transform.localPosition = startPosition + new Vector3(0, 0, z);
    }
}
