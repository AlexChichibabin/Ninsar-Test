using System;
using System.Collections.Generic;

public interface IInventoryPresenter
{
	int Capacity { get; }
	IReadOnlyList<IInventorySlot> Slots { get; }

	event Action InventoryChanged;

	void Dispose();
	void Init();
	void Filter(ItemType? type);
	int AddItem(ItemConfig item, int amount = 1);
	int RemoveItem(ItemConfig item, int amount = 1);
}