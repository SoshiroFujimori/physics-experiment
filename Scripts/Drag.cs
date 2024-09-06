using System;
using UnityEngine;

public class Drag : MonoBehaviour
{
    [SerializeField] float seaLevel = 0;
    [SerializeField] float waterDensity = 997f;
    [SerializeField] Rigidbody rb;
    [SerializeField] float k;

    void Start()
    {
        if (IsOnWaterSurface())
        {
            throw new Exception("水面に付けてください。");
        }

        Vector3 initialForce = CalculateBuoyantForce();
        rb.mass = initialForce.y / -Physics.gravity.y;
    }

    void Update()
    {
        if (IsOnWaterSurface())
        {
            return;
        }

        Vector3 buoyantForce = CalculateBuoyantForce();
        rb.AddForce(buoyantForce, ForceMode.Force);

        // ドラッグの計算と適用
        Vector3 drag = -k * rb.velocity;
        rb.AddForce(drag, ForceMode.Acceleration);
    }

    Vector3 CalculateBuoyantForce()
    {
        float submergedHeight = Mathf.Clamp(transform.localScale.y / 2 - GetCenterToSeaLevelDistance(), 0, transform.localScale.y);
        float submergedVolume = submergedHeight * transform.localScale.z * transform.localScale.x;
        float buoyantForceMagnitude = submergedVolume * waterDensity * -Physics.gravity.y;
        return buoyantForceMagnitude * Vector3.up;
    }

    float GetCenterToSeaLevelDistance()
    {
        return transform.position.y - seaLevel;
    }

    bool IsOnWaterSurface()
    {
        return GetCenterToSeaLevelDistance() > transform.localScale.y / 2;
    }
}
