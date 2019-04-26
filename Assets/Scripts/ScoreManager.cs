using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI text;
    int score;
    public Text winText;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        winText.text = "";
    }

    public void Changescore(int coinValue)
    {
        score += coinValue;
        text.text = "X" + score.ToString();
        if (score >= 12)
        {
            winText.text = "Balanced, as all games should be....";
        }
    }
}
