using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;




public class Scr_ui_spawn_panel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Scr_ui_warning m_warning;
    public Scr_spawn_manager m_spawnManager;
    

    private Animator m_anim;

    // Start is called before the first frame update
    void Start()
    {
        m_spawnManager = FindObjectOfType<Scr_spawn_manager>();
        m_anim = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowPanel(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        ShowPanel(false);
    }

    private void ShowPanel(bool value)
    {
        m_anim.SetBool("bool_showpanel", value);
    }

    public void AddItem(Item item)
    {
        if (item == Item.BANDAID)
        {
            if (m_spawnManager.ItemPlacedOnGround(Item.LAST_RESORT_KNIFE))
                m_spawnManager.AddItemToHand(item);
            else
                m_warning.EnableWarning(WarningType.NEED_KNIFE);
        }
        else
            m_spawnManager.AddItemToHand(item);
    }


}
