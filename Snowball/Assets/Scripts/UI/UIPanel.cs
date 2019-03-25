using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIPanel : MonoBehaviour
{
	[SerializeField]
	private UIPanelHolder UIPanelHolder;
	private List<IFocusUIElement> focusUIElements = new List<IFocusUIElement>();
	private List<GameObject> selectableUIElements = new List<GameObject>();

	public List<GameObject> SelectableUIElements { get { return selectableUIElements; } private set { selectableUIElements = value; } }
	public List<IFocusUIElement> FocusUIElements { get { return focusUIElements; } private set { focusUIElements = value; } }

	private void Start()
	{
		SystemToolMethods.RecursionSearch(gameObject.transform, typeof(Selectable), SelectableUIElements);
		UIPanelHolder.Subscribe(this);
	}
	
}

