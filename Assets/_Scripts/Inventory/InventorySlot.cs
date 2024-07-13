using System;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public static Action<UsableInventoryItemSO> OnAnyItemUsed;
    public static Action<InventorySlot> OnAnySlotClicked;

    [SerializeField] private TextMeshProUGUI amountTextField;
    [SerializeField] private Image itemImageFrame;
    [SerializeField] private Image itemOutline;
    [SerializeField] private Image itemBackground;
    [SerializeField] private Button button;
    [SerializeField] private Color selectedColor;


    private void Start() {
        UpdateSlot();
        SetUnselected();
        OnAnySlotClicked += InventorySlot_OnAnySlotClicked;
    }

    private void InventorySlot_OnAnySlotClicked(InventorySlot slot){
        if(slot == this) {
            SetSelected();
        } else {
            SetUnselected();
        }
    }

    public void SetSelected() {
        itemOutline.color = new Color(selectedColor.r, selectedColor.g, selectedColor.b, .25f);
        itemBackground.color = selectedColor;
    }

    public void SetUnselected() {
        itemOutline.color = new Color(255, 255, 255, .25f);
        itemBackground.color = Color.white;
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
                OnAnySlotClicked?.Invoke(this);
            });
        }

        itemImageFrame.gameObject.SetActive(true);
        amountTextField.text = inventoryItem.GetAmount().ToString();
        itemImageFrame.sprite = inventoryItem.GetItemSO().sprite;

        amountTextField.transform.parent.gameObject.SetActive(!(inventoryItem.GetAmount() == 1));
    }
}