using System;

public interface IInventoryView
{
	event Action<IInventorySlot> SlotClickedAction;

	void Construct(IInventoryPresenter presenter);
	void Init();
	void OnSlotClicked(IInventorySlot slot);
}