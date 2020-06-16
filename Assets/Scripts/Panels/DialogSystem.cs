using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

namespace Panels
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DialogSystem : UIBehaviour
    {
        public Button firstAnswer, secondAnswer;
        public TMPro.TMP_Text textField;
    }

    [Serializable]
    public struct DialogText
    {

    }
}
