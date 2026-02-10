using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI textScore;
    int points = 0;


    void Update()
    {
        textScore.text = "Score: " + points.ToString();
    }

    public void AddPoints(int score) { points += score; }
    public void RemovePoints(int score) { points -= score; }
}
