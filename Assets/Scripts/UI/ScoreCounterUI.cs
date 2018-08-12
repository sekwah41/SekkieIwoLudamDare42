using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class ScoreCounterUI : MonoBehaviour {

        TextMeshProUGUI textComponent;

        // Use this for initialization
        void Start() {
            GameManager.Instance.Points.RegisterOnChangedHandler(HandleScoreChanged);
            textComponent = GetComponent<TextMeshProUGUI>();

            UpdateText(GameManager.Instance.Points.Value);
        }

        void UpdateText(int score)
        {
            textComponent.text = "" + score;
        }

        void HandleScoreChanged(int newScore)
        {
            UpdateText(newScore);
        }
    }
}