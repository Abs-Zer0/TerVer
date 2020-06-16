using Panels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Buttons
{
    [RequireComponent(typeof(Button))]
    public class DropDownButton : UIBehaviour
    {
        public DisappearPanel panel;

        private Button btn;

        protected override void Awake()
        {
            base.Awake();

            this.btn = GetComponent<Button>();
            this.btn.onClick.AddListener(() => Action());
        }

        private void Action()
        {
            if (this.panel != null)
            {
                if (this.panel.IsDisappeared())
                    this.panel.ToNormal();
                else
                    this.panel.ToDisappeared();
            }
        }
    }
}
