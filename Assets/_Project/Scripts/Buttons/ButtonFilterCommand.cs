using UnityEngine;

public class ButtonFilterCommand : ButtonCommand
{
	[SerializeField] private InventoryView inventoryView;

	public void SetFilterAll() => presenter.Filter(null);
	public void SetFilterWeapon() => presenter.Filter(ItemType.Weapon);
	public void SetFilterConsumable() => presenter.Filter(ItemType.Consumable);
	public void SetFilterResource() => presenter.Filter(ItemType.Resource);
	public void SetFilterAmmo() => presenter.Filter(ItemType.Ammo);
}
