using UnityEngine;

public class BounceController : MonoBehaviour
{
    // 反射係数
    [SerializeField] float e = 0.8f; // 1.0fだと完全反射、1未満だとエネルギー損失
    [SerializeField] Rigidbody rb;
    Vector3 prevVelocity = Vector3.zero;

    void Update()
    {
        prevVelocity = rb.velocity;
    }

    // 衝突時の処理
    void OnCollisionEnter(Collision collision)
    {
        rb.velocity = prevVelocity;

        // 衝突点の法線ベクトルを取得
        Vector3 normal = collision.contacts[0].normal;

        // 反射ベクトルを計算
        Vector3 reflectedVelocity = Vector3.Reflect(rb.velocity, normal);

        Debug.Log("Reflected velocity: " + reflectedVelocity);
        Debug.Log("Normal: " + normal);
        Debug.Log("Prev velocity: " + prevVelocity);

        // 反射係数を掛けて速度を調整
        rb.velocity = reflectedVelocity * e;
    }
}
