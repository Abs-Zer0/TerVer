using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Panels
{
    public class MenuPanel : UIBehaviour
    {
        public Button carTmp;
        public string carTmpSceneName = "";
        public Button coinTmp;
        public string coinTmpSceneName = "";
        public Button mainMenu;
        public string mainMenuSceneName = "";
        public Button exit;

        protected override void Awake()
        {
            base.Awake();

            if (this.carTmp != null)
                this.carTmp.onClick.AddListener(() => LoadCarTmp());

            if (this.coinTmp != null)
                this.coinTmp.onClick.AddListener(() => LoadCoinTmp());

            if (this.mainMenu != null)
                this.mainMenu.onClick.AddListener(() => LoadMainMenu());

            if (this.exit != null)
                this.exit.onClick.AddListener(() => Exit());
        }

        private void LoadCarTmp()
        {
            SceneManager.LoadScene(this.carTmpSceneName);
        }

        private void LoadCoinTmp()
        {
            SceneManager.LoadScene(this.coinTmpSceneName);
        }

        private void LoadMainMenu()
        {
            SceneManager.LoadScene(this.mainMenuSceneName);
        }

        private void Exit()
        {
            Application.Quit();
        }
    }
}
