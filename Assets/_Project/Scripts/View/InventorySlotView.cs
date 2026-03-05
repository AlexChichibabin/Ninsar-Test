using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotView : MonoBehaviour, IInventorySlotView
{
	[Header("Components")]
	[SerializeField] private Image iconImage;
	[SerializeField] private Image background;
	[SerializeField] private TMP_Text countText;
	[SerializeField] private Button button;

	[Header("Colors")]
	[SerializeField] private Color emptyBgColor;
	[SerializeField] private Color normalBgColor;

	private IInventorySlot inventorySlot;
	private IInventoryView owner;

	private void Start() => button.onClick.AddListener(OnClick);
	private void OnDestroy() => button.onClick.RemoveListener(OnClick);
	public void Construct(IInventoryView owner) => this.owner = owner;

	public void Enable() => gameObject.SetActive(true);
	public void Disable() => gameObject.SetActive(false);

	public void Render(IInventorySlot slot)
	{
		inventorySlot = slot;

		if (slot.IsEmpty)
		{
			iconImage.enabled = false;
			countText.text = "";
			background.color = emptyBgColor;
			return;
		}

		background.color = normalBgColor;

		iconImage.enabled = true;
		iconImage.sprite = slot.Item.Icon;
		iconImage.color = slot.Item.Color;

		if (slot.Item.Stackable)
			countText.text = slot.Quantity.ToString();
		else
			countText.text = "";
	}
	private void OnClick()
	{
		if (inventorySlot == null) return;

		owner.OnSlotClicked(inventorySlot);
	}
}
