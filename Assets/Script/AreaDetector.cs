using UnityEngine;

public class AreaDetector : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject areaObject;
    public GameObject menu;

    private void Start()
    {
        menu.SetActive(false);
    }
    private void Update()
    {
        DetectInArea();
    }

    private void DetectInArea()
    {
        // ??? targetObject ?? areaObject ?? Transform ???
        Transform targetTransform = targetObject.transform;
        Transform areaTransform = areaObject.transform;

        // ??? targetObject ?? areaObject ????
        Vector3 targetSize = targetObject.GetComponent<Renderer>().bounds.size;
        Vector3 areaSize = areaObject.GetComponent<Renderer>().bounds.size;

        // ???? targetObject ?????
        Vector3 targetMin = targetTransform.position - targetSize / 2f;
        Vector3 targetMax = targetTransform.position + targetSize / 2f;

        // ???? areaObject ?????
        Vector3 areaMin = areaTransform.position - areaSize / 2f;
        Vector3 areaMax = areaTransform.position + areaSize / 2f;

        // ??? targetObject ????????? areaObject ??????
        if (targetMin.x >= areaMin.x && targetMin.y >= areaMin.y && targetMin.z >= areaMin.z &&
            targetMax.x <= areaMax.x && targetMax.y <= areaMax.y && targetMax.z <= areaMax.z)
        {
            Debug.Log($"{targetObject.name} is inside the area of {areaObject.name}.");
        }
        else
        {
            Debug.Log($"{targetObject.name} is outside the area of {areaObject.name}.");
            ActivateTargetObject();
        }
    }

    private void ActivateTargetObject()
    {
        menu.SetActive(true); // ???????????
    }
}