using UnityEngine;

public class AreaDetector : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject areaObject;

    private void Update()
    {
        DetectInArea();
    }

    private void DetectInArea()
    {
        // 获取 targetObject 和 areaObject 的 Transform 信息
        Transform targetTransform = targetObject.transform;
        Transform areaTransform = areaObject.transform;

        // 获取 targetObject 和 areaObject 的尺寸
        Vector3 targetSize = targetObject.GetComponent<Renderer>().bounds.size;
        Vector3 areaSize = areaObject.GetComponent<Renderer>().bounds.size;

        // 计算 targetObject 的边界范围
        Vector3 targetMin = targetTransform.position - targetSize / 2f;
        Vector3 targetMax = targetTransform.position + targetSize / 2f;

        // 计算 areaObject 的边界范围
        Vector3 areaMin = areaTransform.position - areaSize / 2f;
        Vector3 areaMax = areaTransform.position + areaSize / 2f;

        // 检查 targetObject 是否完全位于 areaObject 的边界内
        if (targetMin.x >= areaMin.x && targetMin.y >= areaMin.y && targetMin.z >= areaMin.z &&
            targetMax.x <= areaMax.x && targetMax.y <= areaMax.y && targetMax.z <= areaMax.z)
        {
            Debug.Log($"{targetObject.name} is inside the area of {areaObject.name}.");
        }
        else
        {
            Debug.Log($"{targetObject.name} is outside the area of {areaObject.name}.");
        }
    }
}