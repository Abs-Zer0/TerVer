using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace CarTemplate
{
    namespace Controls
    {
        public class ScorePanel : UIBehaviour
        {
            public string txtCurrent = "", txtSlash = "", txtAll = "";
            public TMPro.TMP_Text field;
            public Color normal = Color.white, correct = Color.black;

            protected override void Start()
            {
                base.Start();

                string current = txtCurrent.Replace("${number}", (0).ToString())
                    .Replace("${color}", ColorUtility.ToHtmlStringRGB(this.normal));
                string slash = txtSlash.Replace("${color}", ColorUtility.ToHtmlStringRGB(this.normal));
                this.txtAll = txtAll.Replace("${number}", GameLogic.resolves.ToString())
                    .Replace("${color}", ColorUtility.ToHtmlStringRGB(this.correct));

                if (this.field != null)
                    this.field.text = current + slash + this.txtAll;
            }

            public void Rewrite(int value)
            {
                Color color;
                if (value == GameLogic.resolves)
                    color = this.correct;
                else
                    color = this.normal;

                string current = txtCurrent.Replace("${number}", value.ToString())
                    .Replace("${color}", ColorUtility.ToHtmlStringRGB(color));
                string slash = txtSlash.Replace("${color}", ColorUtility.ToHtmlStringRGB(color));

                if (this.field != null)
                    this.field.text = current + slash + this.txtAll;
            }
        }
    }
}
