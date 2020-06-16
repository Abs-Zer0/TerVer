using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Containers;

namespace CoinTemplate
{
    namespace Panels
    {
        [RequireComponent(typeof(HSlideView))]
        public class BlocksPanel : UIBehaviour, IPointerClickHandler
        {
            public GameObject prefab;
            public string objectName = "";
            public Block selected;

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
                        Block block = this.slider.Add(this.prefab, this.objectName + " " + (i + 1).ToString()).GetComponent<Block>();
                        block.index = i + 1;
                    }
                }
            }

            public void OnPointerClick(PointerEventData eventData)
            {
                if (IsActionable)
                {
                    GameObject target = eventData.pointerCurrentRaycast.gameObject;
                    Block block = target.GetComponent<Block>();

                    if (block != null)
                    {
                        if (this.selected != null)
                            this.selected.Unselect();

                        if (this.selected == block)
                            this.selected = null;
                        else
                        {
                            this.selected = block;
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
