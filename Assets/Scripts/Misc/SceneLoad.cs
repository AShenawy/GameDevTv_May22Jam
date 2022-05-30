using UnityEngine;

public class SceneLoad : MonoBehaviour
{
    public void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(GameManager.level1);
    }
}
