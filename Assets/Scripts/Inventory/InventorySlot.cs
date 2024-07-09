using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI amountTextField;
    [SerializeField] private Image itemImageFrame;

    public void UpdateSlot() {
        InventoryItem inventoryItem = InventorySystem.Instance.GetItemOnSlot(this);
        if(inventoryItem == null) {
            amountTextField.text = "";
            itemImageFrame.gameObject.SetActive(false);
            return;
        }

        itemImageFrame.gameObject.SetActive(true);
        amountTextField.text = inventoryItem.GetAmount().ToString();
        itemImageFrame.sprite = inventoryItem.GetItemSO().sprite;
    }
}