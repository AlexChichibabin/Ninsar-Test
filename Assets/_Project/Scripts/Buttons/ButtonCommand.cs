using UnityEngine;

public class ButtonCommand : MonoBehaviour
{
	protected IInventoryPresenter presenter;
	protected bool isInited = false;

	public void Construct(IInventoryPresenter presenter)
	{
		if (isInited) return;

		this.presenter = presenter;

		isInited = true;
	}
}
