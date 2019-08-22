using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scr_find_item : MonoBehaviour
{
    public Scr_spawn_manager m_spawnManager;
    public Scr_world_interactable_list m_worldInteractList;


    public Scr_interactable FindItem(Item type)
    {
        for (int i = 0; i < m_spawnManager.m_itemPool.Count; i++)
        {
            var item = m_spawnManager.m_itemPool[i];
            if (item.gameObject.activeInHierarchy)
            {
                if (item.m_itemType.Equals(type))
                    return item;
            }
        }
        return null;
        
    }

    public Scr_interactable FindWorldInteractable(WorldInteract type)
    {
        for (int i = 0; i < m_worldInteractList.m_worldList.Count; i++)
        {
            var item = m_worldInteractList.m_worldList[i];
            if (item.gameObject.activeInHierarchy)
            {
                if (item.m_worldInteractType.Equals(type))
                {
                    return item;
                }
            }

        }
        Debug.LogWarning("World interactable object does not exist!");
        return null;
    }





}
