using System;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public static Action<UsableInventoryItemSO> OnAnyItemUsed;

    [SerializeField] private TextMeshProUGUI amountTextField;
    [SerializeField] private Image itemImageFrame;
    [SerializeField] private Button button;


    private void Start() {
        UpdateSlot();
    }

    public void UpdateSlot() {
        button.onClick.RemoveAllListeners();
        InventoryItem inventoryItem = InventorySystem.Instance.GetItemOnSlot(this);

        if (inventoryItem == null) {
            amountTextField.text = "";
            itemImageFrame.gameObject.SetActive(false);
            amountTextField.transform.parent.gameObject.SetActive(false);
            return;
        }

        if (inventoryItem.GetItemSO() is UsableInventoryItemSO usableInventoryItemSO) {
            button.onClick.AddListener(() => {
                OnAnyItemUsed?.Invoke(usableInventoryItemSO);
            });
        }

        itemImageFrame.gameObject.SetActive(true);
        amountTextField.text = inventoryItem.GetAmount().ToString();
        itemImageFrame.sprite = inventoryItem.GetItemSO().sprite;

        amountTextField.transform.parent.gameObject.SetActive(!(inventoryItem.GetAmount() == 1));
    }
}