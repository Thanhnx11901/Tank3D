
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lose : UICanvas
{
    public Text score;
    public Button mainMenuButton;
    private void Start()
    {
        score.text = "Score:" + GameManager.Instance.score.ToString();
        mainMenuButton.onClick.AddListener(MainMenuButton);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Main");
    }
}
