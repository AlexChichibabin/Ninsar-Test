using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour, IInventoryView
{
	public event Action<IInventorySlot> SlotClickedAction;

	[Header("UI")]
	[SerializeField] private Transform slotsRoot;
	[SerializeField] private InventorySlotView slotPrefab;

	private IInventoryPresenter presenter;
	private readonly List<IInventorySlotView> slotViews = new();

	public void Construct(IInventoryPresenter presenter)
	{
		this.presenter = presenter;
	}

	public void Init()
	{
		presenter.InventoryChanged += RefreshAll;

		SpawnSlotViews();
		RefreshAll();
	}
	public void OnSlotClicked(IInventorySlot slot)
	{
		SlotClickedAction?.Invoke(slot);
	}

	private void OnDestroy()
	{
		if (presenter != null) presenter.InventoryChanged -= RefreshAll;
	}

	private void RefreshAll()
	{
		var filteredSlots = presenter.Slots;

		for (int i = 0; i < filteredSlots.Count; i++)
		{
			slotViews[i].Enable();
			slotViews[i].Render(filteredSlots[i]);
		}
		for (int i = slotViews.Count - 1; i >= filteredSlots.Count; i--) slotViews[i].Disable();
	}

	private void SpawnSlotViews()
	{
		slotViews.Clear();
		for (int i = 0; i < presenter.Capacity; i++)
		{
			var view = Instantiate(slotPrefab, slotsRoot);
			view.Construct(this);
			slotViews.Add(view);
		}
	}
}
