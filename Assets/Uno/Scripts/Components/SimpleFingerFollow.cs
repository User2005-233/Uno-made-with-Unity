using UnityEngine;

[ExecuteAlways] // 允许在非运行状态（Edit Mode）下实时执行
public class SimpleFingerFollow : MonoBehaviour
{
    [Header("1. 你的手指根部主控骨骼")]
    public Transform fingerRoot;

    [Header("2. 需要跟随旋转的子关节（按顺序：从指根到指尖）")]
    public Transform[] childJoints;

    [Header("3. 每一节手指的弯曲放大倍数 (例如 1.0, 1.2, 1.5)")]
    public float[] multipliers;

    // 缓存上一次的旋转，用来检测是否在 Editor 中被手动旋转了
    private Quaternion lastRootRotation;

    void Update()
    {
        if (fingerRoot == null || childJoints == null) return;

        // 如果主控骨骼的旋转发生了变化（无论是 Play 还是 Edit 模式）
        if (fingerRoot.localRotation != lastRootRotation)
        {
            lastRootRotation = fingerRoot.localRotation;

            // 获取根部在 Z 轴上的旋转角度（或者你需要的轴）
            float rootZ = fingerRoot.localEulerAngles.z;

            for (int i = 0; i < childJoints.Length; i++)
            {
                if (childJoints[i] == null) continue;

                // 获取倍率，如果没填默认为 1.0
                float mult = (multipliers != null && i < multipliers.Length) ? multipliers[i] : 1.0f;

                // 获取子关节当前的旋转，保持 X 和 Y 不变，只修改 Z 轴
                Vector3 currentAngles = childJoints[i].localEulerAngles;
                
                // 让子关节按比例跟着根部的 Z 轴一起转
                currentAngles.z = rootZ * mult;
                
                childJoints[i].localEulerAngles = currentAngles;
            }
        }
    }
}
