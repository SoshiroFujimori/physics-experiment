using System;
using UnityEngine;

public class Buoyancy : MonoBehaviour
{
    // 海面の高さを示す値を『seaLevel』という名前で用意している
    // 『[SerializeField]』を使うことで、Unityの画面上からこの値を設定できるようにしている
    [SerializeField] float seaLevel = 0;

    // 水の密度を示す値を『waterDensity』という名前で用意している
    // 『[SerializeField]』を使うことで、Unityの画面上からこの値を設定できるようにしている
    [SerializeField] float waterDensity = 997f;

    // 物体に関する情報を入れるための箱を『rb』という名前で用意している
    // この箱には、物理演算を使って物体を動かすための情報が入る
    // 『[SerializeField]』を使うことで、Unityの画面上からこの値を設定できるようにしている
    [SerializeField] Rigidbody rb;

    // ゲームが開始されたときに1回だけ実行される処理の塊
    // 『{}』で囲まれた範囲が、1回分の処理内容
    void Start()
    {
        // 物体が水面にいるかどうかを確認するためのチェック
        // 『IsOnWaterSurface』（90行目で用意された）を使って、もし水面上にあればエラーを発生させる
        if (IsOnWaterSurface())
        {
            throw new Exception("水面に付けてください。");
        }

        // 浮力を計算し、その値を初期の力として用いる
        // 『CalculateBuoyantForce』（60行目で用意された）を使って浮力を計算
        Vector3 initialForce = CalculateBuoyantForce();

        // 浮力に基づいて物体の質量を設定する
        // 『initialForce』（32行目で用意された）のy成分を利用して、『rb』（17行目で用意された）の質量を設定する
        // この設定は、浮力 (f) = 物体の質量 (m) × 重力加速度 (a) という考え方（f=ma）に基づいており、
        // m = f/a の形に変形して質量を求めている
        rb.mass = initialForce.y / -Physics.gravity.y;
    }

    // ゲーム画面が1回更新されるごとに実行される処理の塊
    // 『{}』で囲まれた範囲が、1回分の更新ごとに実行される内容
    void Update()
    {
        // 物体が水面にいる場合、何もしないで終了する
        // 『IsOnWaterSurface』（90行目で用意された）を使って、もし水面上にいれば、この後の処理は行わない
        if (IsOnWaterSurface())
        {
            return;
        }

        // 浮力を再計算して物体に適用する
        // 『CalculateBuoyantForce』（60行目で用意された）を使って新しい浮力を計算し、『rb』（17行目で用意された）に力として加える
        Vector3 buoyantForce = CalculateBuoyantForce();
        rb.AddForce(buoyantForce, ForceMode.Force);
    }

    // 浮力を計算するための処理の塊
    // 『{}』で囲まれた範囲が、浮力を計算するための内容
    Vector3 CalculateBuoyantForce()
    {
        // 水面と物体の中心の距離を基に、物体が水中にどれだけ沈んでいるかの高さを計算
        // このとき、計算された高さが0以下にならないように、0と物体の高さの間で制限される
        // 『Mathf.Clamp』は、値を最小値と最大値の間に収めるための処理
        float submergedHeight = Mathf.Clamp(transform.localScale.y / 2 - GetCenterToSeaLevelDistance(), 0, transform.localScale.y);

        // 沈んでいる部分の体積を計算
        // 体積は、沈んでいる高さ×幅×奥行きで求められる
        float submergedVolume = submergedHeight * transform.localScale.z * transform.localScale.x;

        // 浮力の大きさを計算
        // 浮力は『浮力の公式』を使って求める
        // 浮力 = 沈んでいる部分の体積 × 水の密度 × 重力加速度
        float buoyantForceMagnitude = submergedVolume * waterDensity * -Physics.gravity.y;

        // 浮力の大きさと方向をベクトルで返す
        return buoyantForceMagnitude * Vector3.up;
    }

    // 物体の中心から海面までの距離を計算するための処理の塊
    // 『{}』で囲まれた範囲が、距離を計算するための内容
    float GetCenterToSeaLevelDistance()
    {
        // 物体の位置（transform.position）と海面の高さ（8行目で用意された『seaLevel』）を使って距離を計算
        return transform.position.y - seaLevel;
    }

    // 物体が水面にいるかどうかを確認するための処理の塊
    // 『{}』で囲まれた範囲が、水面にいるかを確認するための内容
    bool IsOnWaterSurface()
    {
        // 物体の中心と海面の距離が、物体の高さの半分より大きいかどうかをチェック
        return GetCenterToSeaLevelDistance() > transform.localScale.y / 2;
    }
}
