using UnityEngine;

[CreateAssetMenu(fileName = "ItemConfig", menuName = "Configs/ItemConfig")]
public class ItemConfig : ScriptableObject
{
	public string Id => id;
	public string Name => displayName;
	public string Description => description;
	public ItemType Type => type;
	public bool Stackable => stackable;
	public int MaxStackSize => stackable ? maxStackSize : 1;
	public Sprite Icon => icon;
	public Color Color => color;


    [SerializeField] private string id;
	[SerializeField] private string displayName;
	[SerializeField] private ItemType type;
	[SerializeField] private bool stackable;
	[SerializeField] private int maxStackSize;
	[SerializeField] private Sprite icon;
	[SerializeField] private Color color;
	[TextArea][SerializeField] private string description;

#if UNITY_EDITOR
	private void OnValidate()
	{
		if (!stackable) maxStackSize = 1;
		if (stackable && maxStackSize < 2) maxStackSize = 2;
		if (string.IsNullOrWhiteSpace(id))
			id = name;
	}
#endif
}
