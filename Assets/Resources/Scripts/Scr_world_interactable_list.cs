using System.Collections.Generic;
using UnityEngine;

public class Scr_world_interactable_list : MonoBehaviour
{
    public List<Scr_interactable> m_worldList = new List<Scr_interactable>();

    private void Awake()
    {
        foreach (var item in GetComponentsInChildren<Scr_interactable>())
        {
            m_worldList.Add(item);
        }
    }


}
