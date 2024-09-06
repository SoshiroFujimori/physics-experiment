using UnityEngine;

// f = kx（バネの力の公式）
public class Spring : MonoBehaviour
{
    // 物体に関する情報を入れるための箱を『rb』という名前で用意している
    // この箱には、物理演算を使って物体を動かすための情報が入る
    // 『[SerializeField]』を使うことで、Unityの画面上からこの値を設定できるようにしている
    [SerializeField] Rigidbody rb;

    // バネの強さを表す値を入れるための箱を『k』という名前で用意している
    // この値はバネ定数（バネがどれだけ強い力を発揮するか）を表す
    // 『[SerializeField]』を使うことで、Unityの画面上からこの値を設定できるようにしている
    [SerializeField] float k;

    // 初期位置（バネが伸びたり縮んだりする前の位置）を入れるための箱を『basePos』という名前で用意している
    Vector3 basePos;

    // ゲームが始まったときに1回だけ実行される処理の塊
    // 『{}』で囲まれた範囲が、ゲーム開始時に実行される内容
    void Start()
    {
        // 現在の物体の位置を『basePos』（17行目で宣言）に保存する
        basePos = transform.position;
    }

    // ゲーム画面が1回更新されるごとに実行される処理の塊
    // 『{}』で囲まれた範囲が、1回分の更新ごとに実行される内容
    void Update()
    {
        // 『basePos』（17行目で宣言）から現在の物体の位置を引いた差（物体がどれだけ動いたか）を『delta』という名前の箱に入れる
        Vector3 delta = basePos - transform.position;

        // 『delta』（32行目で宣言）に『k』（14行目で宣言）の値を掛けて、その結果を『force』という名前の箱に入れる
        // これにより、バネの力（力の大きさと方向）が計算される
        Vector3 force = delta * k;

        // 『rb』（9行目で宣言）に対して『force』（36行目で宣言）の力を、『ForceMode.Force』で加える
        rb.AddForce(force, ForceMode.Force);
    }
}
