using UnityEngine;

public class MyAddForce : MonoBehaviour
{
    // どの方向にどれだけの力を加えるかを指定するための箱を『force』という名前で用意している
    // この箱には、x、y、zの3つの値（方向と大きさ）が入る
    // 『[SerializeField]』を使うことで、Unityの画面上からこの値を設定できるようにしている
    [SerializeField] Vector3 force;

    // 物体の質量を指定するための箱を『mass』という名前で用意している
    // この値は、力の影響をどれくらい受けるかを決める
    // 『[SerializeField]』を使うことで、Unityの画面上からこの値を設定できるようにしている
    [SerializeField] float mass;

    // どの種類の力を加えるかを指定するための箱を『forceMode』という名前で用意している
    // この箱には、『ForceMode.Force』、『ForceMode.Acceleration』、『ForceMode.Impulse』、『ForceMode.VelocityChange』という値のどれかが入る
    // 『[SerializeField]』を使うことで、Unityの画面上からこの値を設定できるようにしている
    [SerializeField] ForceMode forceMode;

    // 現在の速度を保存するための箱を『velocity』という名前で用意している
    // 初期状態では速度はゼロとしている
    Vector3 velocity = Vector3.zero;

    // ゲーム画面が1回更新されるごとに実行される処理の塊
    // 『{}』で囲まれた範囲が、1回分の更新ごとに実行される内容
    void Update()
    {
        // 『forceMode』（18行目で用意）が『Force』か『Acceleration』の場合、『{}』内を実行
        if (forceMode == ForceMode.Force || forceMode == ForceMode.Acceleration)
        {
            // 『force』（8行目で用意）と『forceMode』（18行目で用意）を用いて力を加える処理の塊を実行
            AddMyForce(force, forceMode);
        }
        // それ以外の場合（『forceMode』が『Impulse』または『VelocityChange』の場合）で、タブキーが押されたとき、『{}』内を実行
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            // 『force』（8行目で用意）と『forceMode』（18行目で用意）を用いて力を加える処理の塊を実行
            AddMyForce(force, forceMode);
        }

        // 現在の位置に『velocity』（22行目で用意）と時間の積を足して、物体を移動させる
        // 『Time.deltaTime』は前回の『AddMyForce』が実行されてから経過した秒数を表す
        // これにより、時間の経過に応じた移動が実現される
        transform.position += velocity * Time.deltaTime;
    }

    // 力を加える処理の塊
    // 『{}』で囲まれた範囲が、力を加える内容
    void AddMyForce(Vector3 force, ForceMode mode)
    {
        // 『mode』（49行目で用意、32行目または38行目で割り当て）に応じて異なる処理を行う
        switch (mode)
        {
            case ForceMode.Force:
                // 力の大きさ×時間÷質量を『velocity』（22行目で用意）に足し算する
                // 『Time.deltaTime』は前回の『AddMyForce』から経過した秒数を表す
                velocity += force * Time.deltaTime / mass;
                break;
            case ForceMode.Impulse:
                // 力の大きさ÷質量を『velocity』（22行目で用意）に足し算する
                velocity += force / mass;
                break;
            case ForceMode.Acceleration:
                // 力の大きさ×時間を『velocity』（22行目で用意）に足し算する
                // 『Time.deltaTime』は前回の『AddMyForce』から経過した秒数を表す
                velocity += force * Time.deltaTime;
                break;
            case ForceMode.VelocityChange:
                // 力の大きさをそのまま『velocity』（22行目で用意）に足し算する
                velocity += force;
                break;
        }
    }
}