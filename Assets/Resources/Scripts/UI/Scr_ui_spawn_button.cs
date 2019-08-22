using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Scr_ui_spawn_button : MonoBehaviour, IPointerClickHandler
{
    public Item m_itemType;

    public void OnPointerClick(PointerEventData eventData)
    {
        Scr_ui_spawn_panel spawnPanel = GetComponentInParent<Scr_ui_spawn_panel>();
        spawnPanel.AddItem(m_itemType);
    }
}
