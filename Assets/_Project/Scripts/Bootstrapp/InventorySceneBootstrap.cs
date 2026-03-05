using UnityEngine;

public class InventorySceneBootstrap : MonoBehaviour
{
	[SerializeField] private InventoryView inventoryView;
	[SerializeField] private ButtonCommand[] buttonCommand;

	private IInventory inventory;
	private IInventoryPresenter presenter;
	
	private void Awake()
	{
		Create();
		Inject();
		Init();
	}
	private void OnDestroy()
	{
		presenter.Dispose();
	}
	private void Create()
	{
		inventory = new Inventory(20);
		presenter = new InventoryPresenter(inventory, inventoryView);
	}
	private void Inject()
	{
		inventoryView.Construct(presenter);
		foreach (var command in buttonCommand)
			command.Construct(presenter);
	}
	private void Init()
	{
		inventoryView.Init();
		presenter.Init();
	}
}
