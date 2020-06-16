using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("raycast: " + eventData.pointerCurrentRaycast.gameObject);
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.GetComponent<BoxCollider2D>());
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
