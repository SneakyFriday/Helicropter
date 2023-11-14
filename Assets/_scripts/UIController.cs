using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText.text = GameManager.Instance.Score.ToString();
        PlayerController.OnCropped.AddListener(UpdateScore);
        PlayerController.OnPumpkinCropped.AddListener(UpdateHigherScore);
        GameManager.Instance.RestartGameEvent.AddListener(ResetScore);
    }

    private void UpdateScore()
    {
        GameManager.Instance.Score++;
        scoreText.text = GameManager.Instance.Score.ToString();
    }
    
    private void ResetScore()
    {
        GameManager.Instance.Score = 0;
        scoreText.text = GameManager.Instance.Score.ToString();
    }

    private void UpdateHigherScore()
    {
        GameManager.Instance.Score += 10;
        scoreText.text = GameManager.Instance.Score.ToString();
    }
    
}
