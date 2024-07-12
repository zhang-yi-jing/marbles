using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class howtoplayLogic : MonoBehaviour
{
    private int clickCount = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickCount++;

            if (clickCount > 2)
            {
                Destroy(gameObject);
            }
        }
    }
}