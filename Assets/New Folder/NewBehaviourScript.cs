using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour,IDropHandler
{
    public GameObject Informator;
    public GameObject Memory;
   public bool enable=true;
    public void OnDrop(PointerEventData eventData)
    {
        
            Debug.Log("Ondrop");
            if (eventData.pointerDrag != null&& Memory==null)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            Memory = eventData.pointerDrag;
            Debug.Log(eventData.pointerDrag.GetComponent<Info>().number);
                Informator.GetComponent<Getinfo>().GetInformation(eventData.pointerDrag.GetComponent<Info>().number, true);
                
            }else
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.GetComponent<DragItem>().begin.anchoredPosition;
            eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts=false;
            eventData.pointerDrag.GetComponent<DragItem>().DestroyAnton();
        }

    }
    
}
