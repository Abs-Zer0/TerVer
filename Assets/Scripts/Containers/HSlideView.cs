using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace Containers
{
    public class HSlideView : SlideView
    {
        protected override void Awake()
        {
            base.Awake();

            this.rectTransform.pivot = new Vector2(0, 0.5f);
            this.rectTransform.localPosition = new Vector3(-this.rectTransform.rect.width * 0.5f, 0, 0);

            GetComponent<BoxCollider2D>().offset = new Vector2(this.rectTransform.rect.width * 0.5f, 0);
        }

        protected override void CheckOutOfEdges()
        {
            if (this.rectTransform.rect.width < this.contentPanel.rect.width)
            {
                if (this.contentPanel.localPosition.x > 0)
                    this.contentPanel.localPosition = new Vector3(0, 0, 0);

                if (this.contentPanel.localPosition.x < this.rectTransform.rect.width - this.contentPanel.rect.width)
                    this.contentPanel.localPosition = new Vector3(this.rectTransform.rect.width - this.contentPanel.rect.width, 0, 0);
            }
            else
                this.contentPanel.localPosition = Vector3.zero;
        }

        protected override void Drag(PointerEventData eventData)
        {
            this.contentPanel.localPosition += new Vector3(eventData.delta.x * this.scrollSpeed * Time.deltaTime, 0, 0);
        }

        protected override void Scroll(PointerEventData eventData)
        {
            this.contentPanel.localPosition += new Vector3(-eventData.scrollDelta.y * this.scrollSpeed * 30f * Time.deltaTime, 0, 0);
        }

        protected override void SetContentPanel()
        {
            this.contentPanel.anchorMin = new Vector2(0, 0);
            this.contentPanel.anchorMax = new Vector2(0, 1);
            this.contentPanel.sizeDelta = Vector2.zero;
            this.contentPanel.pivot = new Vector2(0, 0.5f);
            this.contentPanel.localPosition = Vector3.zero;
            this.contentPanel.ForceUpdateRectTransforms();
        }

        protected override void UpdateChildren()
        {
            Vector3 pos = Vector3.zero;
            for (int i = 0; i < this.children.Count; i++)
            {
                this.children[i].anchorMin = new Vector2(0, 0.5f);
                this.children[i].anchorMax = new Vector2(0, 0.5f);

                if (this.controlSize)
                {
                    float scale = this.contentPanel.rect.height / this.children[i].rect.height;
                    this.children[i].sizeDelta = new Vector2(this.children[i].sizeDelta.x, scale * this.children[i].sizeDelta.y);
                    if (this.preserveAspect)
                        this.children[i].sizeDelta = new Vector2(scale * this.children[i].sizeDelta.x, this.children[i].sizeDelta.y);
                    this.children[i].ForceUpdateRectTransforms();
                }

                pos += new Vector3(this.children[i].rect.width * 0.5f + this.spacing, 0, 0);
                this.children[i].localPosition = pos;
                pos += new Vector3(this.children[i].rect.width * 0.5f + this.spacing, 0, 0);
            }

            this.contentPanel.sizeDelta = new Vector2(pos.x, 0);
            this.contentPanel.localPosition = Vector3.zero;
        }
    }
}
