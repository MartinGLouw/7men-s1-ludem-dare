using UnityEngine;
using UnityEngine.EventSystems; 

public class HoverDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject panelToDisplay; 

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (panelToDisplay != null)
            panelToDisplay.SetActive(true); 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (panelToDisplay != null)
            panelToDisplay.SetActive(false); 
    }
}
