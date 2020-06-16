using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Containers;

namespace CoinTemplate
{
    namespace Panels
    {
        [RequireComponent(typeof(HSlideView))]
        public class CoinsPanel : UIBehaviour, IPointerClickHandler
        {
            public GameObject prefab;
            public string objectName = "";
            public Coin selected;

            public bool IsActionable { get; set; } = true;

            private HSlideView slider;

            protected override void Awake()
            {
                base.Awake();

                this.slider = GetComponent<HSlideView>();
            }

            protected override void Start()
            {
                base.Start();

                if (this.prefab != null)
                {
                    int count = (int)Mathf.Pow(2, GameLogic.throws);
                    for (int i = 0; i < count; i++)
                    {
                        Coin coin = this.slider.Add(this.prefab, this.objectName + " " + (i + 1).ToString()).GetComponent<Coin>();
                        coin.index = i + 1;
                    }
                }
            }

            public void OnPointerClick(PointerEventData eventData)
            {
                if (IsActionable)
                {
                    GameObject target = eventData.pointerCurrentRaycast.gameObject;
                    Coin coin = target.GetComponent<Coin>();

                    if (coin != null)
                    {
                        if (this.selected != null)
                            this.selected.Unselect();

                        if (this.selected == coin)
                            this.selected = null;
                        else
                        {
                            this.selected = coin;
                            this.selected.Select();
                        }
                    }
                }
            }

            public void ToNonActionable()
            {
                IsActionable = false;
            }
        }
    }
}
