using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace Containers
{
    [RequireComponent(typeof(Image), typeof(BoxCollider2D))]
    public abstract class SlideView : UIBehaviour, IDragHandler, IScrollHandler
    {
        public GameObject prefab;
        public bool controlSize = false;
        public bool preserveAspect = true;
        public RectTransform contentPanel;
        [Min(0)]
        public int spacing = 0;
        [Range(1, 100)]
        public int scrollSpeed = 1;

        public bool IsActionable { get; set; } = true;

        protected List<RectTransform> children;
        protected RectTransform rectTransform;

        protected override void Awake()
        {
            base.Awake();

            this.rectTransform = GetComponent<RectTransform>();
            GetComponent<BoxCollider2D>().size = this.rectTransform.rect.size;

            if (this.contentPanel != null)
            {
                if (this.contentPanel.parent != this.rectTransform)
                    this.contentPanel.SetParent(this.rectTransform);

                SetContentPanel();

                this.children = new List<RectTransform>();
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (this.contentPanel != null)
            {
                Drag(eventData);
                CheckOutOfEdges();
            }
        }

        public void OnScroll(PointerEventData eventData)
        {
            if (this.contentPanel != null)
            {
                Scroll(eventData);
                CheckOutOfEdges();
            }
        }

        public RectTransform Add(string name)
        {
            return Add(this.prefab, name);
        }

        public RectTransform Add(GameObject pref)
        {
            return Add(pref, pref.name);
        }

        public RectTransform Add(GameObject pref, string name)
        {
            if (this.contentPanel != null && pref != null)
            {
                this.children.Add(Canvas.Instantiate(pref, this.contentPanel).GetComponent<RectTransform>());
                this.children[this.children.Count - 1].gameObject.name = name;
                UpdateChildren();

                return this.children[this.children.Count - 1];
            }
            else
                return null;
        }

        protected abstract void SetContentPanel();
        protected abstract void UpdateChildren();
        protected abstract void Drag(PointerEventData eventData);
        protected abstract void Scroll(PointerEventData eventData);
        protected abstract void CheckOutOfEdges();
    }
}
