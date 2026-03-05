using System;
using System.Collections.Generic;

public interface IInventory
{
	int Capacity { get; }
	IReadOnlyList<IInventorySlot> Slots { get; }

	event Action InventoryChanged;

	int AddItem(ItemConfig item, int amount = 1);
	int RemoveItem(ItemConfig item, int amount = 1);
	bool TryRemoveItem(IInventorySlot slot);
	void Filter(ItemType? type);
}