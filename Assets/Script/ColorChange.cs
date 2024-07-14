using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public GameObject targetObject;
    private CollisionDetector collisionDetector;
    private float proximityPercentage;
    private bool isInTriggerRange;
    // Start is called before the first frame update
    void Start()
    {
        collisionDetector = targetObject.GetComponent<CollisionDetector>();
        
    }

    // Update is called once per frame
    void Update()
    {
        proximityPercentage = collisionDetector.proximityPercentage;
        isInTriggerRange = collisionDetector.isInTriggerRange;
        if (isInTriggerRange)
        {
            if (proximityPercentage <= 0.35f)
            {
                Color newColor = GetComponent<SpriteRenderer>().color;
                newColor.a = 90f / 255f; // Set alpha value to 120 (out of 255)
                GetComponent<SpriteRenderer>().color = newColor;
            }
            else if (proximityPercentage <= 0.55f)
            {
                Color newColor = GetComponent<SpriteRenderer>().color;
                newColor.a = 160f / 255f; // Set alpha value to 180 (out of 255)
                GetComponent<SpriteRenderer>().color = newColor;
            }
            else
            {
                // Reset the alpha value to its original value or any desired default value
                Color newColor = GetComponent<SpriteRenderer>().color;
                newColor.a = 1f; // Assuming the original alpha value is 1 (fully opaque)
                GetComponent<SpriteRenderer>().color = newColor;
            }
        }
        else 
        {
            Color newColor = GetComponent<SpriteRenderer>().color;
            newColor.a = 0f; // Set alpha value to 120 (out of 255)
            GetComponent<SpriteRenderer>().color = newColor;
        }
    }
}
