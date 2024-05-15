using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyDestroyer : MonoBehaviour
{
    public CandyManger candyManger;
    public int reward;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Candy")
        {
            //指定数だけCandyのストックを増やす
            candyManger.AddCandy(reward);


            //オブジェクトを破壊
            Destroy(other.gameObject);
        }
    }
}
