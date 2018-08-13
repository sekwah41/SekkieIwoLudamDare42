using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public BackgroundUI backgroundUI;
        public GameObject menuBits;
        public GameObject gameOverBits;
        public Slider comboSlider;
        public TextMeshProUGUI highScoreCounter;

        void Awake()
        {
            Instance = this;
        }

        public void ShowMenuBits()
        {
            menuBits.SetActive(true);
        }

        public void HideMenuBits()
        {
            menuBits.SetActive(false);
        }

        public void ShowGameOverBits()
        {
            gameOverBits.SetActive(true);
            highScoreCounter.text = "" + GameManager.Instance.HighScore;
        }

        public void HideGameOverBits()
        {
            gameOverBits.SetActive(false);
        }
    }
}