using TMPro;
using UnityEngine;
using YG;

public class UpdateTopScore : MonoBehaviour
{
    [SerializeField] private TMP_Text topScoreText;
    void Start()
    {
        RefreshTopScore();
    }

    public void RefreshTopScore()
    {
       // topScoreText.text = YG2.saves.TopScore.ToString();
    }

}
