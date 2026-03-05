using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : IInventory
{
	public event Action InventoryChanged;
	public int Capacity => slots.Length;
	public IReadOnlyList<IInventorySlot> Slots => FilterByType(currentFilter);

	private readonly IInventorySlot[] slots;
	private ItemType? currentFilter = null;
	private bool IsFull => slots.All(s => !s.IsEmpty);

	public Inventory(int capacity = 20)
	{
		if (capacity <= 0) capacity = 20;
		slots = new InventorySlot[capacity];
		for (int i = 0; i < capacity; i++)
			slots[i] = new InventorySlot();
	}

	public void Filter(ItemType? type)
	{
		currentFilter = type;
		InventoryChanged?.Invoke();
	}

	public int AddItem(ItemConfig item, int amount = 1)
	{
		if (item == null) return 0;
		if (amount <= 0) return 0;
		

		int remaining = amount;

		if (item.Stackable)
		{
			for (int i = 0; i < slots.Length && remaining > 0; i++)
			{
				var s = slots[i];
				if (s.CanStackWith(item))
				{
					int added = s.AddToStack(remaining);
					remaining -= added;
				}
			}
		}

		if (IsFull && remaining > 0)
		{
			Debug.Log("Can't add item: inventory is full");
			return amount - remaining;
		}

		for (int i = 0; i < slots.Length && remaining > 0; i++)
		{
			var s = slots[i];
			if (!s.IsEmpty) continue;

			int put = item.Stackable ? Mathf.Min(item.MaxStackSize, remaining) : 1;
			s.Set(item, put);
			remaining -= put;
		}

		int addedTotal = amount - remaining;
		if (addedTotal > 0) InventoryChanged?.Invoke();
		return addedTotal;
	}
	public int RemoveItem(ItemConfig item, int amount = 1)
	{
		if (item == null)
		{
			Debug.Log("Can't remove item: sended item is null");
			return 0;
		}

		if (amount <= 0) return 0;

		int remainAmount = amount;

		for (int i = slots.Length - 1; i >= 0 && remainAmount > 0; i--)
		{
			var s = slots[i];
			if (s.IsEmpty) continue;
			if (s.Item != item) continue;

			int removed = s.RemoveFromStack(remainAmount);
			remainAmount -= removed;
		}

		int removedTotal = amount - remainAmount;
		if (removedTotal > 0) InventoryChanged?.Invoke();
		else Debug.Log("Can't remove item: inventory doesn't contain the item");
		return removedTotal;
	}
	public bool TryRemoveItem(IInventorySlot slot)
	{
		if (slot == null)
		{
			Debug.Log("Can't remove item: sended slot is null");
			return false;
		}

		bool isRemoved = slot.RemoveFromStack(1) > 0;

		InventoryChanged?.Invoke();

		return isRemoved;
	}


	private List<IInventorySlot> FilterByType(ItemType? type)
	{
		var result = new List<IInventorySlot>(slots.Length);

		for (int i = 0; i < slots.Length; i++)
		{
			var s = slots[i];
			if (!s.IsEmpty)
				if (s.Item.Type == type || type == null)
					result.Add(s);
		}
		for (int i = 0; i < slots.Length; i++)
		{
			var s = slots[i];
			if (s.IsEmpty) result.Add(s);
		}

		return result;
	}
}
