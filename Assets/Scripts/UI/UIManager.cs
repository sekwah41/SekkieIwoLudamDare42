using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public BackgroundUI backgroundUI;
        public GameObject menuBits;
        public Slider comboSlider;

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
    }
}