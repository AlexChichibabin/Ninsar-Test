using TMPro;
using UnityEngine;

public class AmountChangeView : MonoBehaviour
{
    [SerializeField] private AmountChange amountChange;
	[SerializeField] private TextMeshProUGUI text;

	void Start() => amountChange.AmountChanged.AddListener(OnAmountChanged);
	private void OnDestroy() => amountChange.AmountChanged.RemoveListener(OnAmountChanged);
	private void OnAmountChanged(int amount) => text.text = amount.ToString();
}
