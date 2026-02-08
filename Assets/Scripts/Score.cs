using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI textScore;
    [HideInInspector] public int points = 0;


    void Update()
    {
        textScore.text = "Score: " + points.ToString();
    }
}
