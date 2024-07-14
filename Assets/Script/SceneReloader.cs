using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ReloadScene();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToFirstScene();
        }
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        // 获取当前场景的索引
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // 重新加载当前场景
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void ReturnToFirstScene()
    {
        Time.timeScale = 1f;
        // 加载第一个场景（索引为 0）
        SceneManager.LoadScene(0);
    }
}