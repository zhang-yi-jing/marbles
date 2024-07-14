using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class howtoplayLogic : MonoBehaviour
{
    public CollisionDetector collisionDetector;
    public TextMeshProUGUI textMeshPro;
    private Coroutine blinkCoroutine; // 用于存储BlinkText协程的引用
    private int previousMessage; // 用于存储message的上一个值

    void Start()
    {
        previousMessage = collisionDetector.m_messge;
    }

    void Update()
    {
        if (collisionDetector.m_messge != previousMessage) // 只有当message的值改变时，才执行以下操作
        {
            switch (collisionDetector.m_messge)
            {
                case 0:
                    textMeshPro.text = "";
                    StopBlinking(); // 如果message是0，就停止闪烁
                    break;
                case 1:
                    textMeshPro.text = "Click or Space anywhere to LOCK";
                    StartBlinking(); // 如果message是1，就开始闪烁
                    break;
                case 2:
                    textMeshPro.text = "Click or Space anywhere to RELEASE";
                    StartBlinking(); // 如果message是2，就开始闪烁
                    break;
            }
            previousMessage = collisionDetector.m_messge; // 更新previousMessage的值
        }
    }

    void StartBlinking()
    {
        StopBlinking(); // 在开始新的闪烁协程之前，先停止正在运行的闪烁协程
        blinkCoroutine = StartCoroutine(BlinkText());
    }

    void StopBlinking()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine); // 停止正在运行的闪烁协程
        }
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            textMeshPro.enabled = !textMeshPro.enabled;
            yield return new WaitForSeconds(0.5f);
        }
    }
}