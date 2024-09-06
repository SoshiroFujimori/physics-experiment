using UnityEngine;

public class AddForceExample : MonoBehaviour
{
    // 物体に関する情報を入れるための箱を『rb』という名前で用意している
    // この箱には、物理演算を使って物体を動かすための情報が入る
    // 『[SerializeField]』を使うことで、Unityの画面上からこの値を設定できるようにしている
    [SerializeField] Rigidbody rb;

    // どの方向にどれだけの力を加えるかを指定するための箱を『force』という名前で用意している
    // この箱には、x、y、zの3つの値（方向と大きさ）が入る
    // 『[SerializeField]』を使うことで、Unityの画面上からこの値を設定できるようにしている
    [SerializeField] Vector3 force;

    // どの種類の力を加えるかを指定するための箱を『forceMode』という名前で用意している
    // この箱には、『ForceMode.Force』、『ForceMode.Acceleration』、『ForceMode.Impulse』、『ForceMode.VelocityChange』という値のどれかが入る
    // 『[SerializeField]』を使うことで、Unityの画面上からこの値を設定できるようにしている
    [SerializeField] ForceMode forceMode;

    // ゲーム画面が1回更新されるごとに実行される処理の塊
    // 『{}』で囲まれた範囲が、1回分の更新ごとに実行される内容
    void Update()
    {
        // 『forceMode』（18行目で宣言）が『Force』か『Acceleration』の場合、『{}』内を実行
        if (forceMode == ForceMode.Force || forceMode == ForceMode.Acceleration)
        {
            // 『rb』（8行目で宣言）に対して『force』（13行目で宣言）の力を、『forceMode』（18行目で宣言）に基づいて加える
            rb.AddForce(force, forceMode);
        }
        // それ以外の場合（『forceMode』が『Impulse』または『VelocityChange』の場合）で、tabキーが押されているとき、『{}』内を実行
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            // 『rb』（8行目で宣言）に対して『force』（13行目で宣言）の力を、『forceMode』（18行目で宣言）に基づいて加える
            rb.AddForce(force, forceMode);
        }
        // 上記の2つの『if』および『else if』の条件に従い、それぞれの条件が真であれば対応する処理の塊が実行される
    }
}
