using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CraftingRecipe : MonoBehaviour
{
    private Transform container;

    private void Awake()
    {
        container = transform.Find("IngredientData_Container");

        container.gameObject.SetActive(false);
    }

    public void CreateCraftingList(List<Item> items)
    {
        foreach(Item item in items)
        {
            Transform dataTransform = Instantiate(container, this.transform);
            dataTransform.gameObject.SetActive(true);

            dataTransform.Find("IngredientDataIcon").GetComponent<Image>().sprite = item.getSprite();
            dataTransform.Find("IngredientDataAmount").GetComponent<TextMeshProUGUI>().SetText(item.amount.ToString());

        }
    }
}
