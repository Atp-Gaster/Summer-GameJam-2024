using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    //GameOver Scene is in Game Over Indicator

    public void LoadSceneTitle()
    {
        SceneManager.LoadScene("GameTitle");
    }

    public void LoadSceneGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
