using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    [SerializeField] private Character Player;
    [SerializeField] private GameStateManager SaveData;


    [SerializeField] private Canvas InventoryUI;

    [SerializeField] private GameObject ItemButtonPrefab;

    [SerializeField]List<RectTransform> Tabs;


    private void Start()
    {
        SaveData.LoadState();
        UpdateItemList(SaveData.CurrentGameState.CurrentInventory.Weapons , Tabs[0],0 );
        UpdateItemList(SaveData.CurrentGameState.CurrentInventory.Armors, Tabs[1],1);
        UpdateItemList(SaveData.CurrentGameState.CurrentInventory.Consumables, Tabs[2],2);
        UpdateItemList(SaveData.CurrentGameState.CurrentInventory.KeyItems, Tabs[3],3);

        
        
        InventoryUI.gameObject.SetActive(false);
    }

    
    
    private void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (InventoryUI.isActiveAndEnabled)
            {
                InventoryUI.gameObject.SetActive(false);
            }
            else
            {
                InventoryUI.gameObject.SetActive(true);
            }
        }
    }





    public void UpdateItemList(List<int> ItemList , RectTransform parent, int index )
    {
        //make new ones
        for (int i = 0; i < ItemList.Count; i++)
        {

            GameObject temp=Instantiate(ItemButtonPrefab, parent);
            temp.transform.parent = parent.GetChild(1).GetChild(0).GetChild(0);
            temp.transform.position = new Vector3(520,800,0);
            temp.transform.position += new Vector3(0,  -i  *120,0);

            temp.GetComponent<ItemButton>().InventoryMenu = this;


            if (index == 0)
            {
                temp.GetComponent<ItemButton>().Item = ItemLibrary.Instance.Weapons[ItemList[i]];   
            }else if (index == 1)
            {
                temp.GetComponent<ItemButton>().Item = ItemLibrary.Instance.Armors[ItemList[i]];   
            }else if (index == 2)
            {
                temp.GetComponent<ItemButton>().Item = ItemLibrary.Instance.Consumables[ItemList[i]];   
            }else 
            {
                temp.GetComponent<ItemButton>().Item = ItemLibrary.Instance.KeyItems[ItemList[i]];   
            }

        }

    
    }




    public void Equip()
    {

    }

    public void Unequip()
    {

    }

    public void CloseUI()
    {

    }

    public void UpdateItemList()
    {
        // Update written list of items

    }

    public void SortItems()
    {
        // select a certain list of items and sort it alphabetically
        //then showcase in a UI list
    }

}
