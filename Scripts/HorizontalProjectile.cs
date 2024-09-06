using UnityEngine;

public class HorizontalProjectile : MonoBehaviour
{
    // 物体に関する情報を入れるための箱を『rb』という名前で用意している
    // この箱には、物理演算を使って物体を動かすための情報が入る
    // 『[SerializeField]』を使うことで、Unityの画面上からこの値を設定できるようにしている
    [SerializeField] Rigidbody rb;

    // 目標となる位置を指定するための箱を『targetPos』という名前で用意している
    // この箱には、物体が向かうべき最終的な位置のx、y、zの3つの値が入る
    // 『[SerializeField]』を使うことで、Unityの画面上からこの値を設定できるようにしている
    [SerializeField] Vector3 targetPos;

    // ゲームが開始されたときに1回だけ実行される処理の塊
    // 『{}』で囲まれた範囲が、1回分の処理内容
    void Start()
    {
        // 現在の位置（『transform.position』）と目標位置（13行目で用意された『targetPos』）を使って、初速度を計算する。29行目にジャンプ。
        Vector3 initVelocity = CalculateInitVelocity(transform.position, targetPos);

        // 『rb』（8行目で用意された）に対して、計算した初速度を即時的な速度変化として加える
        rb.AddForce(initVelocity, ForceMode.VelocityChange);
    }

    // 水平投射の初速度を計算する処理の塊
    // 『{}』で囲まれた範囲が、初速度を計算するための処理
    // 29行目の『launchPos』と『targetPos』は、現在の位置と目標位置を表す箱であり、20行目で割り当てられる。
    Vector3 CalculateInitVelocity(Vector3 launchPos, Vector3 targetPos)
    {
        // 現在の位置からxとzの値を取り出し、高さを無視した2Dの位置を計算
        Vector3 launchPosXZ = new(launchPos.x, 0, launchPos.z);

        // 目標位置からxとzの値を取り出し、高さを無視した2Dの位置を計算
        Vector3 targetPosXZ = new(targetPos.x, 0, targetPos.z);

        // 現在位置と目標位置の水平距離を計算
        float horizontalDistance = Vector3.Distance(launchPosXZ, targetPosXZ);

        // 現在の高さと目標の高さの差を計算（垂直方向の落差）
        float verticalDrop = launchPos.y - targetPos.y;

        // 初速度の水平成分を計算
        // 式は v0 = √(g / 2y)|x| に対応している
        // ここで、gは重力加速度（-Physics.gravity.y）であり、yは垂直方向の落差（verticalDrop）、xは水平距離（horizontalDistance）である
        float initHorizontalSpeed = Mathf.Sqrt(-Physics.gravity.y / (2 * verticalDrop)) * Mathf.Abs(horizontalDistance);

        // 現在の位置（32行目で用意された『launchPosXZ』）から目標位置（35行目で用意された『targetPosXZ』）に向かう方向ベクトルを計算
        Vector3 initDirection = (targetPosXZ - launchPosXZ).normalized;

        // 方向ベクトルに初速度の大きさを掛け算して、最終的な初速度を計算して返す
        return initDirection * initHorizontalSpeed;
    }
}
