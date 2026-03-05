using System;
using System.Collections.Generic;

public class InventoryPresenter : IInventoryPresenter, IDisposable
{
	public event Action InventoryChanged;
	public int Capacity => inventory.Capacity;
	public IReadOnlyList<IInventorySlot> Slots => inventory.Slots;


	private IInventory inventory;
	private IInventoryView inventoryView;

	public InventoryPresenter(
		IInventory inventory,
		IInventoryView inventoryView)
	{
		this.inventory = inventory;
		this.inventoryView = inventoryView;
	}
	public void Init()
	{
		inventoryView.SlotClickedAction += OnSlotClicked;
		inventory.InventoryChanged += InventoryChanged;
	}
	public void Dispose()
	{
		inventoryView.SlotClickedAction -= OnSlotClicked;
		inventory.InventoryChanged -= InventoryChanged;
	}
	public void Filter(ItemType? type) =>
		inventory.Filter(type);
	public int AddItem(ItemConfig item, int amount = 1) =>
		inventory.AddItem(item, amount);
	public int RemoveItem(ItemConfig item, int amount = 1) =>
		inventory.RemoveItem(item, amount);
	

	private void OnSlotClicked(IInventorySlot slot) =>
		inventory.TryRemoveItem(slot);

}
