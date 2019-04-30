using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIPanel : MonoBehaviour
{
	[SerializeField]
	private ISubject uIPanelHolder;

	public List<Selectable> selectables = new List<Selectable>();
	public List<Selectable> SelectableUIElements { get; private set; } = new List<Selectable>();
	public List<IFocusUIElement> FocusUIElements { get; private set; } = new List<IFocusUIElement>();

	private void Start()
	{	
		uIPanelHolder = GameObject.Find("MainCanvas").GetComponent<ISubject>();
		SystemToolMethods.TransformRecursionSearch(gameObject.transform, SelectableUIElements);
		uIPanelHolder.Subscribe(this);
		selectables = SelectableUIElements;
	}
	
}

