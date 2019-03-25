using System.Collections.Generic;
using UnityEngine;

public class UIPanelHolder : MonoBehaviour, ISubject
{
	public Transform canvasTransform;
	[HideInInspector]
	public List<UIPanel> initializedUIPanels = new List<UIPanel>();

	private UIPanel startPanel;
	public UIPanel StartPanel { get { return startPanel; } private set { startPanel = value; } }

	private void Start()
	{
		StartPanel = null;
	}

	public void Subscribe<T>(T item)
	{
		if (item is UIPanel uIPanel)
		{
			initializedUIPanels.Add(uIPanel);
			if (StartPanel == null)
			{
				StartPanel = initializedUIPanels[0];
			}
		}
	}

	public void UnSubscribe<T>(T item)
	{
		if (item is UIPanel uIPanel)
		{
			initializedUIPanels.Remove(uIPanel);
		}
	}
}
