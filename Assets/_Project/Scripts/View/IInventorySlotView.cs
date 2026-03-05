public interface IInventorySlotView
{
	void Construct(IInventoryView owner);
	public void Enable();
	public void Disable();
	void Render(IInventorySlot slot);
}