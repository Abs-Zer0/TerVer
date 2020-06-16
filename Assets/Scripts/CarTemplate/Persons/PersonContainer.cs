using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Containers;

namespace CarTemplate
{
    namespace Persons
    {
        [RequireComponent(typeof(VSlideView))]
        public class PersonContainer : UIBehaviour
        {
            public GameObject normal, cap;
            public string objectName = "";

            private VSlideView slider;

            protected override void Awake()
            {
                base.Awake();

                this.slider = GetComponent<VSlideView>();
            }

            protected override void Start()
            {
                base.Start();

                if (this.normal != null && this.cap != null)
                    AddPersons();
            }

            private void AddPersons()
            {
                for (int i = 0; i < GameLogic.places; i++)
                {
                    PersonSpawner spawner;
                    if (i < GameLogic.caps)
                        spawner = this.slider.Add(this.cap).GetComponent<PersonSpawner>();
                    else
                        spawner = this.slider.Add(this.normal).GetComponent<PersonSpawner>();

                    spawner.SetColor(GameLogic.colors[i]);
                    spawner.name = this.objectName + " " + ColorUtility.ToHtmlStringRGB(GameLogic.colors[i]);
                }
            }
        }
    }
}
