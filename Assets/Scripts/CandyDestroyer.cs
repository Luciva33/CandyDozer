using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyDestroyer : MonoBehaviour
{
    public CandyManger candyManger;
    public int reward;
    //エフェクトプレハブプロパティ
    public GameObject effectPrefab;
    //エフェクトローテーションプロパティ
    public Vector3 effectRotation;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Candy")
        {
            //指定数だけCandyのストックを増やす
            candyManger.AddCandy(reward);


            //オブジェクトを破壊
            Destroy(other.gameObject);

            //エフェクトプレハブの設定チェック
            if (effectPrefab != null)
            {
                //Candyのポジションにエフェクト生成
                Instantiate(
                    effectPrefab,
                    other.transform.position,
                    // Quaternion.Eulerで向きを変更(Candy.effectRotationを取得)
                    Quaternion.Euler(effectRotation)
                );
            }
        }
    }
}
