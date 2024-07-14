using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    public void ReloadScene()
    {
        Time.timeScale = 1f;
        // 获取当前场景的索引
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // 重新加载当前场景
        SceneManager.LoadScene(currentSceneIndex);

        
    }
}