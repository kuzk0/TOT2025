using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour,IDropHandler
{
    public GameObject Informator;
    public GameObject Memory,Mesto;
   public bool enable=true;
    public void OnDrop(PointerEventData eventData)
    {
        
            Debug.Log("Ondrop");
            if (eventData.pointerDrag != null&& Memory==null&& eventData.pointerDrag.GetComponent<Info>() != null)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                Memory = eventData.pointerDrag;
                
                Informator.GetComponent<Getinfo>().GetInformation(eventData.pointerDrag.GetComponent<Info>().number, true);
                
               
            Memory.transform.parent = Mesto.transform;
            Memory.GetComponent<RectTransform>().anchoredPosition = Mesto.GetComponent<RectTransform>().anchoredPosition;
        }
        else
        {
            if(eventData.pointerDrag.GetComponent<DragItem>() != null) 
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.GetComponent<DragItem>().begin.anchoredPosition;
            if (eventData.pointerDrag.GetComponent<CanvasGroup>() != null)
                eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts=false;
            if (eventData.pointerDrag.GetComponent<DragItem>() != null)
                eventData.pointerDrag.GetComponent<DragItem>().DestroyAnton();
        }

    }
    
}
