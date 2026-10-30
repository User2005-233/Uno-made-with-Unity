using UnityEngine;

[ExecuteAlways] // 允许在非运行状态（Edit Mode）下实时执行
public class FullRotationCopy : MonoBehaviour
{
    [Header("主控骨骼（例如根部关节）")]
    public Transform masterBone;

    [Header("跟随的目标子骨骼")]
    public Transform targetBone;

    void LateUpdate()
    {
        if (masterBone == null ||targetBone == null) return;

        // 完全复制主控骨骼的局部旋转（包含 X、Y、Z 所有轴）
        targetBone.localRotation = masterBone.localRotation;
    }
}
