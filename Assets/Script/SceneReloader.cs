using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    public void ReloadScene()
    {
        // 获取当前场景的索引
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // 重新加载当前场景
        SceneManager.LoadScene(currentSceneIndex);

        
    }
}