public interface IInventorySlot
{
	bool IsEmpty { get; }
	ItemConfig Item { get; }
	int Quantity { get; }

	int AddToStack(int amount);
	bool CanStackWith(ItemConfig other);
	void Clear();
	int RemoveFromStack(int amount);
	void Set(ItemConfig newItem, int newQuantity);
}