using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using CarTemplate.Persons;

namespace CarTemplate
{

    public class Platform : UIBehaviour, IPointerClickHandler
    {
        public PersonSlot[] slots = new PersonSlot[0];

        public bool IsActionable { get; set; } = true;

        private PersonSpawner selected;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (IsActionable)
            {
                GameObject target = eventData.pointerCurrentRaycast.gameObject;
                PersonSpawner spawner = target.GetComponent<PersonSpawner>();
                PersonSlot slot = target.GetComponent<PersonSlot>();

                if (spawner != null)
                    SelectSpawner(spawner);

                if (slot != null)
                    SelectSlot(slot);
            }
        }

        public bool equals(Platform other)
        {
            if (this.slots.Length != other.slots.Length)
                return false;

            bool res = true;
            for (int i = 0; i < this.slots.Length; i++)
                if (!this.slots[i].equals(other.slots[i]))
                {
                    res = false;
                    break;
                }

            return res;
        }

        private void SelectSpawner(PersonSpawner spawner)
        {
            if (this.selected == spawner)
            {
                this.selected.Unselect();
                this.selected = null;
            }
            else
            {
                if (this.selected != null)
                    this.selected.Unselect();

                this.selected = spawner;
                this.selected.Select();
            }

            ShowSlots();
        }

        private void SelectSlot(PersonSlot slot)
        {
            if (slot.IsEngaged())
            {
                slot.UnsetPerson();
            }
            else if (this.selected != null)
            {
                if ((slot.isDriverPlace && this.selected.isCap) || !slot.isDriverPlace)
                {
                    if (this.selected.IsEngaged())
                        this.selected.GetSlot().UnsetPerson();

                    slot.SetPerson(this.selected);
                }
            }
        }

        private void ShowSlots()
        {
            if (this.selected != null)
            {
                bool isCap = this.selected.isCap;
                foreach (var slot in this.slots)
                    if ((!isCap && !slot.isDriverPlace) || isCap)
                        slot.Select();
                    else
                        slot.Unselect();
            }
            else
            {
                foreach (var slot in this.slots)
                    slot.Unselect();
            }
        }
    }
}
