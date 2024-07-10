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
        // ��ȡ targetObject �� areaObject �� Transform ��Ϣ
        Transform targetTransform = targetObject.transform;
        Transform areaTransform = areaObject.transform;

        // ��ȡ targetObject �� areaObject �ĳߴ�
        Vector3 targetSize = targetObject.GetComponent<Renderer>().bounds.size;
        Vector3 areaSize = areaObject.GetComponent<Renderer>().bounds.size;

        // ���� targetObject �ı߽緶Χ
        Vector3 targetMin = targetTransform.position - targetSize / 2f;
        Vector3 targetMax = targetTransform.position + targetSize / 2f;

        // ���� areaObject �ı߽緶Χ
        Vector3 areaMin = areaTransform.position - areaSize / 2f;
        Vector3 areaMax = areaTransform.position + areaSize / 2f;

        // ��� targetObject �Ƿ���ȫλ�� areaObject �ı߽���
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