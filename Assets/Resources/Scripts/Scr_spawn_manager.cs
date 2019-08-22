using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_spawn_manager : MonoBehaviour
{
    public GameObject m_startPlanPanel;
    public Scr_ui_warning m_warning;
    public GameObject m_itemInHand;
    public List<Scr_item_pickup> m_itemPool;
    public float m_cellRadius;
    public Vector2Int m_grid;
    public Vector2Int m_gridOffset;
    [HideInInspector]
    public bool m_canStartGoap = false;

    private int m_itemPlacedCount = 0;


    public void AddItemToHand(Item item)
    {
        foreach (var i in m_itemPool)
        {
            if (i.m_itemType.Equals(item))
            {
                m_itemInHand = i.gameObject;
            }
        }
    }
    public bool ItemPlacedOnGround(Item item)
    {
        for (int i = 0; i < m_itemPool.Count; i++)
        {
            if (m_itemPool[i].m_itemType == item && m_itemPool[i].gameObject.activeInHierarchy)
                return true;
        }
        return false;
    }

    private void Update()
    {
        if (m_itemInHand != null && Input.GetMouseButtonDown(0))
        {
            PlaceItem();
        }

        // Just for restarting
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    public void PlaceItem()
    {
        if (m_itemPlacedCount < 3)
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 10f;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            //mousePos.x = Mathf.Clamp(Mathf.RoundToInt(mousePos.x), m_gridOffset.x, (m_grid.x + m_gridOffset.x));
            //mousePos.z = Mathf.Clamp(Mathf.RoundToInt(mousePos.z), m_gridOffset.y, (m_grid.y + m_gridOffset.y));
            if (WithinRange(mousePos.x, m_gridOffset.x, m_grid.x + m_gridOffset.x) && WithinRange(mousePos.z, m_gridOffset.y, m_grid.y + m_gridOffset.y))
            {

                foreach (var item in m_itemPool)
                {
                    if (m_itemInHand != null)
                    {
                        if (item.name.Equals(m_itemInHand.name))
                        {
                            item.gameObject.SetActive(true);
                            Vector3 spawnPos = mousePos;
                            spawnPos.y = 1;
                            item.gameObject.transform.position = spawnPos;
                            m_itemInHand = null;
                            m_canStartGoap = true;
                            m_startPlanPanel.SetActive(true);
                            m_itemPlacedCount++;
                        }
                    }
                }

            }
        } else
        {
            m_warning.EnableWarning(WarningType.TOO_MANY_ITEMS);
        }
    }

    private bool WithinRange(float value, float min, float max)
    {
        return (value >= min && value <= max);
    }
    
    private void OnDrawGizmos()
    {
        for (int i = 0; i < m_grid.x; i++)
        {
            for (int j = 0; j < m_grid.y; j++)
            {
                Vector3 cellPos = new Vector3(i+m_gridOffset.x,0,j+m_gridOffset.y);
                Gizmos.color = Color.green;
                Gizmos.DrawCube(cellPos,Vector3.one);
                
            }
            
        }
    }
}
