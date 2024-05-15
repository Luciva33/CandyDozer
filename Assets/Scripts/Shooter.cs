using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    //キャンディプレハブプロパティの配列化
    public GameObject[] candyPrefabs;
    public Transform candyParentTransform;
    public CandyManger candyManager;

    public float shotForce;
    public float shotTorque;
    public float baseWidth;

    void Update()
    {
        //left Ctrlかマウスクリック
        if (Input.GetButtonDown("Fire1")) Shot();
    }

    //キャンディのプレハブからランダムに一つ選ぶ
    //戻り値の型　↓のGameObjectは戻り値の型　voidは戻り値なしを意味
    GameObject SampleCandy()
    {
        int index = Random.Range(0, candyPrefabs.Length);
        return candyPrefabs[index];
    }

    //発射位置の計算
    Vector3 GetInstantiatePosition()

    {
        //画面サイズとInput の割合からキャンディ生成のポジションを計算
        float x = baseWidth *
        //5*(0-2.5)
        //普通に記述しなかったら画面外にも飛んでいくので、baseWidthの幅内に収める
        (Input.mousePosition.x / Screen.width) - (baseWidth / 2);
        return transform.position + new Vector3(x, 0, 0);
    }


    public void Shot()
    {
        //キャンディを生成できる条件外ならSHOTしない
        if (candyManager.GetCandyAmount() <= 0) return;

        //プレハブからCandyオブジェクト生成
        GameObject candy = (GameObject)Instantiate(
            SampleCandy(),
           GetInstantiatePosition(),
            //アタッチされているシューターの位置に生成 this.gameObject.transform.position
            Quaternion.identity
        //shooterの向きと同じ向き
        //四次元数　クォータニオン
        //Quaternionは回転を表現する構造体でQuaternion.identityは回転なし
        // Quaternion.Euler(0f,0f,0f);   //Quaternion
        );


        //親オブジェクトの設定
        //生成したCandyオブジェクトの親をcandyParentTransformに設定する
        //Scriptで動的に親子関係を結ぶ
        candy.transform.parent = candyParentTransform;



        //CandyオブジェクトのRigidbodyを取得し力と回転を加える
        Rigidbody candyRigidBody = candy.GetComponent<Rigidbody>();
        //フォースとトルクの加算
        candyRigidBody.AddForce(transform.forward * shotForce);
        //transform.forward　z軸に向いている方向に飛ばす
        // candyRigidBody.AddTorque(new Vector3(0, shotTorque, 0));
        candyRigidBody.AddTorque(new Vector3(shotTorque, shotTorque, shotTorque));

        //candyのストックを消費
        candyManager.ConsumeCandy();

    }

}
