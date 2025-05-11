using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DragItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] public Canvas canvas;
    public RectTransform rectTransform;
    public CanvasGroup canvasGroup;
    public RectTransform begin;
    public GameObject Anton;
    public Transform canvas1;

    public GameObject sourceDataButton;

    private bool spawn = true;

    private GameObject tempButton;

    private void Start()
    {
        canvasGroup.blocksRaycasts = true;
        transform.name = "Button(Clone)";
    }

    private void Update()
    {
        if (sourceDataButton != null && sourceDataButton.GetComponent<DragItem>().sourceDataButton != null)
        {
            sourceDataButton = sourceDataButton.GetComponent<DragItem>().sourceDataButton;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        if (spawn)
        {
            tempButton = Instantiate(eventData.pointerDrag, canvas1);
        }
        if (Anton != null)
        {
            Anton.GetComponentInParent<NewBehaviourScript>().Informator.GetComponent<Getinfo>().GetInformation(0, false);
            Anton.GetComponentInParent<NewBehaviourScript>().Memory = null;
            Anton = null;
        }
        transform.parent = canvas.transform;
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {


        if (eventData.pointerEnter.tag != "Interact" || eventData.pointerEnter == null)
        {
            eventData.pointerDrag.GetComponent<DragItem>().sourceDataButton = tempButton;

            Destroy(eventData.pointerDrag);
            Debug.Log(eventData.pointerDrag + ";" + eventData.pointerEnter);
        }
        else
        {
            if (tempButton != null)
            {
                eventData.pointerDrag.GetComponent<DragItem>().sourceDataButton = tempButton;

            }
            spawn = false;
            Anton = eventData.pointerEnter;
        }
        tempButton = null;
        canvasGroup.blocksRaycasts = true;
    }
    public void OnDrop(PointerEventData eventData)
    {
    }
    public void DestroyGanton()
    {
        Anton = null;
        canvasGroup.blocksRaycasts = true;
    }
    public void DestroyAnton()
    {
        Invoke("DestroyGanton", 0.5f);
    }
}
