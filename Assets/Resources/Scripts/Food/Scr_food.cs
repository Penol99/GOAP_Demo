using UnityEngine;
using System.Collections.Generic;
public enum FoodType
{
    CARROT_SOUP,
    CARROT_OMELETTE,
    SUNNY_SIDES,
    QUAIL_ROAST,
    QUAIL_STEW,
    SEARED_ARM
}

public class Scr_food : MonoBehaviour
{
    public FoodType m_foodType;

    private static List<Scr_food> FoodList = FoodList = new List<Scr_food>();
    private static Transform m_activeFood;
    private static Transform m_originalParent;

    private void Awake()
    {
        FoodList.Clear();
    }


    private void Start()
    {
        FoodList.Add(this);
        m_originalParent = transform.parent;
        gameObject.SetActive(false);
    }

    public static Transform ActivateFood(FoodType food, Transform parent)
    {
        foreach (var item in FoodList)
        {
            if (item.m_foodType.Equals(food))
            {
                item.gameObject.SetActive(true);
                item.transform.parent = parent;
                item.transform.localPosition = Vector3.zero;
                m_activeFood = item.transform;
                return m_activeFood;
            }
        }
        Debug.LogWarning("Food doesnt exist!");
        return null;
    }
    public static Transform GetActiveFood()
    {
        return m_activeFood;
    }
    public static void DeactivateFood()
    {
        m_activeFood.parent = m_originalParent;
        m_activeFood.gameObject.SetActive(false);
    }
}
