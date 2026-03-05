using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AmountChange : MonoBehaviour
{
	public int Amount => amount;

	[HideInInspector] public UnityEvent<int> AmountChanged;

    [SerializeField] private Button buttonLess;
    [SerializeField] private Button buttonMore;
	[SerializeField] private int maxAmount;

	private int amount = 1;

    void Start()
    {
        buttonLess.onClick.AddListener(OnButtonLess);
		buttonMore.onClick.AddListener(OnButtonMore);
	}
	private void OnDestroy()
	{
		buttonLess.onClick.RemoveListener(OnButtonLess);
		buttonMore.onClick.RemoveListener(OnButtonMore);
	}

	private void OnButtonLess()
	{
		if (amount <= 1)
		{
			amount = 1;
			return;
		}

		amount--;
		AmountChanged?.Invoke(amount);
	}
	private void OnButtonMore()
	{
		if (amount >= maxAmount)
		{
			amount = maxAmount;
			return;
		}
		amount++;
		AmountChanged?.Invoke(amount);
	}
}
