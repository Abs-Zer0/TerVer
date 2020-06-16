using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Containers;
using System.Collections.Generic;
using System.Linq;

namespace CarTemplate
{
    namespace Containers
    {
        [RequireComponent(typeof(CarouselView))]
        public class PlatformsCarouselView : UIBehaviour
        {
            public string pathToPrefab = "", nameOfPrefab = "";

            private GameObject pref;
            private CarouselView carousel;

            protected override void Awake()
            {
                base.Awake();

                this.carousel = GetComponent<CarouselView>();
            }

            protected override void Start()
            {
                base.Start();

                this.pref = Resources.Load<GameObject>(this.pathToPrefab + "/" + this.nameOfPrefab + " " + GameLogic.places);
                this.carousel.SetPrefab(this.pref);
            }

            public List<Platform> GetCars()
            {
                return this.carousel.GetChildren().Select(el => el.GetComponent<Platform>()).ToList();
            }

            public void ToNonActionable()
            {
                this.carousel.IsActionable = false;
                foreach (var car in GetCars())
                    if (car != null)
                        car.IsActionable = false;
            }
        }
    }
}
