using UnityEngine;

public class InventorySlot : IInventorySlot
{
	public ItemConfig Item => item;
	public int Quantity => quantity;
	public bool IsEmpty => item == null || quantity <= 0;

	[SerializeField] private ItemConfig item;

	private int quantity;

	public bool CanStackWith(ItemConfig other)
	{
		if (other == null) return false;
		if (IsEmpty) return false;
		if (!item.Stackable) return false;
		if (item != other) return false;
		return quantity < item.MaxStackSize;
	}

	public void Set(ItemConfig newItem, int newQuantity)
	{
		if (newItem == null || newQuantity <= 0)
		{
			Clear();
			return;
		}

		item = newItem;
		quantity = Mathf.Clamp(newQuantity, 1, newItem.MaxStackSize);
	}

	public void Clear()
	{
		item = null;
		quantity = 0;
	}

	public int AddToStack(int amount)
	{
		if (amount <= 0) return 0;
		if (IsEmpty) return 0;
		if (!item.Stackable) return 0;

		int add = Mathf.Min(amount, item.MaxStackSize - quantity);
		quantity += add;
		return add;
	}

	public int RemoveFromStack(int amount)
	{
		if (amount <= 0) return 0;
		if (IsEmpty) return 0;

		int remove = Mathf.Min(amount, quantity);
		quantity -= remove;

		if (quantity <= 0) Clear();
		return remove;
	}
}
