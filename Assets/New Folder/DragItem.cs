using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DragItem : MonoBehaviour, IPointerDownHandler,IBeginDragHandler,IEndDragHandler,IDragHandler,IDropHandler
{
    [SerializeField] public Canvas canvas;
    public RectTransform rectTransform;
    public CanvasGroup canvasGroup;
    public RectTransform begin;
    public GameObject Anton;
    public Transform canvas1;
    private bool spawn=true;
    private void Start()
    {
        canvasGroup.blocksRaycasts = true;
        transform.name = "Button(Clone)";
    }
    public void OnPointerDown(PointerEventData eventData)
    {
       
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        if(spawn)Instantiate(eventData.pointerDrag,canvas1);
        if (Anton != null)
        {
           Anton.GetComponentInParent<NewBehaviourScript>().Informator.GetComponent<Getinfo>().GetInformation(0, false);
            Anton.GetComponentInParent<NewBehaviourScript>().Memory=null;
            Anton = null;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        
        if((eventData.pointerEnter==null|| eventData.pointerEnter.tag!="Interact"))
        {
            Destroy(eventData.pointerDrag);
            Debug.Log(eventData.pointerDrag + ";" + eventData.pointerEnter);
        }else
        {
            spawn = false;
            Anton = eventData.pointerEnter;
            
        }
        canvasGroup.blocksRaycasts = true;
    }
    public void OnDrop(PointerEventData eventData)
    {

    }
    public void DestroyGanton()
    {
        Anton=null;
        canvasGroup.blocksRaycasts = true;
    }
    public void DestroyAnton()
    {
        Invoke("DestroyGanton",0.5f);
    }
}
