using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public static Action<UsableInventoryItemSO> OnAnyItemUsed;

    [SerializeField] private TextMeshProUGUI amountTextField;
    [SerializeField] private Image itemImageFrame;
    private Button button;

    private void Awake() {
        button = GetComponent<Button>();
    }

    public void UpdateSlot() {
        InventoryItem inventoryItem = InventorySystem.Instance.GetItemOnSlot(this);

        if(inventoryItem == null) {
            amountTextField.text = "";
            itemImageFrame.gameObject.SetActive(false);
            return;
        }

        if(inventoryItem.GetItemSO() is UsableInventoryItemSO) {
            UsableInventoryItemSO usableInventoryItemSO = (UsableInventoryItemSO)inventoryItem.GetItemSO();
            button.onClick.AddListener(usableInventoryItemSO.CreateUsableInstance().OnClick);
            OnAnyItemUsed?.Invoke(usableInventoryItemSO);
        }

        itemImageFrame.gameObject.SetActive(true);
        amountTextField.text = inventoryItem.GetAmount().ToString();
        itemImageFrame.sprite = inventoryItem.GetItemSO().sprite;
    }
}