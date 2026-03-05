using UnityEngine;

public class ButtonChangeCommand : ButtonCommand
{
    [SerializeField] private ItemConfig itemConfig;
	[SerializeField] private AmountChange amountChange;

	private int amountOfChange = 1;

	public void AddItem() => presenter.AddItem(itemConfig, amountOfChange);
	public void RemoveItem() => presenter.RemoveItem(itemConfig, amountOfChange);

	private void Start() =>
		amountChange?.AmountChanged.AddListener(OnAmountChanged);
	private void OnDestroy() =>
		amountChange?.AmountChanged.RemoveListener(OnAmountChanged);

	private void OnAmountChanged(int amount)
	{
		amountOfChange = amount;
	}
}
