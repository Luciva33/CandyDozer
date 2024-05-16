using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    //ショットパワーの回復時間接続
    const int MaxShotPower = 5;
    //ショットパワーの回復時間定数
    const int RecoverySeconds = 3;

    int shotPower = MaxShotPower;

    AudioSource shotSound;


    //キャンディプレハブプロパティの配列化
    public GameObject[] candyPrefabs;
    public Transform candyParentTransform;
    public CandyManger candyManager;

    public float shotForce;
    public float shotTorque;
    public float baseWidth;

    void Start()
    {
        shotSound = GetComponent<AudioSource>();
    }

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
        //ショットパワーチェック
        if (shotPower <= 0) return;


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
        //ショットパワーを消費
        ConsumePower();

        //サウンドを再生
        shotSound.Play();

    }
    // ショットパワーの表示
    void OnGUI()
    {
        GUI.color = Color.black;

        //ShotPowerの残数を+の数で表示
        string label = "";
        for (int i = 0; i < shotPower; i++) label = label + "+";
        GUI.Label(new Rect(50, 65, 100, 30), label);
    }

    //ショットパワーの消費処理
    void ConsumePower()
    {
        //shotPowerを消費すると同時に回復のカウントをスタート
        shotPower--;
        StartCoroutine(RecoverPower());
    }

    //ショットパワーの回復コルーチン
    IEnumerator RecoverPower()
    {
        //一定数秒待った後、shotPowerを回復
        yield return new WaitForSeconds(RecoverySeconds);
        shotPower++;
    }

}
