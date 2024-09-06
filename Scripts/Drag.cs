using System;
using UnityEngine;

public class Drag : MonoBehaviour
{
    // 海面の高さを設定します
    [SerializeField] float seaLevel = 0;
    // 水の密度を設定します
    [SerializeField] float waterDensity = 997f;
    // オブジェクトに付ける物理演算用の部品
    [SerializeField] Rigidbody rb;
    // ドラッグ（抵抗力）の係数
    [SerializeField] float k;

    void Start()
    {
        // オブジェクトが水面に浮いているか確認します
        if (IsOnWaterSurface())
        {
            // 浮いていたらエラーを出します
            throw new Exception("水面に付けてください。");
        }

        // 浮力を計算します
        Vector3 initialForce = CalculateBuoyantForce();
        // 浮力に基づいてオブジェクトの質量を設定します
        rb.mass = initialForce.y / -Physics.gravity.y;
    }

    void Update()
    {
        // オブジェクトが水面に浮いているか確認します
        if (IsOnWaterSurface())
        {
            // 浮いていたら何もしません
            return;
        }

        // 浮力を計算してオブジェクトに加えます
        Vector3 buoyantForce = CalculateBuoyantForce();
        rb.AddForce(buoyantForce, ForceMode.Force);

        // ドラッグ（抵抗力）を計算してオブジェクトに加えます
        Vector3 drag = -k * rb.velocity;
        rb.AddForce(drag, ForceMode.Acceleration);
    }

    // 浮力を計算する関数
    Vector3 CalculateBuoyantForce()
    {
        // オブジェクトのどれだけが水に浸かっているかを計算します
        float submergedHeight = Mathf.Clamp(transform.localScale.y / 2 - GetCenterToSeaLevelDistance(), 0, transform.localScale.y);
        // 浸かっている部分の体積を計算します
        float submergedVolume = submergedHeight * transform.localScale.z * transform.localScale.x;
        // 浮力の大きさを計算します
        float buoyantForceMagnitude = submergedVolume * waterDensity * -Physics.gravity.y;
        // 浮力を上向きのベクトルとして返します
        return buoyantForceMagnitude * Vector3.up;
    }

    // オブジェクトの中心から海面までの距離を計算する関数
    float GetCenterToSeaLevelDistance()
    {
        return transform.position.y - seaLevel;
    }

    // オブジェクトが水面に浮いているかどうかを確認する関数
    bool IsOnWaterSurface()
    {
        return GetCenterToSeaLevelDistance() > transform.localScale.y / 2;
    }
}