using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Start()
    {
        Score.Instance.OnScoreChange += Score_OnScoreChange; ;
    }

    private void Score_OnScoreChange(object sender, Score.OnScoreChangeEventArgs e)
    {
        _scoreText.SetText(e.score.ToString("D3"));
    }
}
