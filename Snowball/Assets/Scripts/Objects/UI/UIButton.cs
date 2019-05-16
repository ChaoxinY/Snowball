using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIButton : MonoBehaviour
{
	#region Variables
	public List<GameObject> panelsToOpen, panelsToClose;
	public string sceneToOpen;
	public enum ButtonFunction
	{
		OpenPanel,
		ClosePanel,
		ExitGame,
		LoadScene,
		OpenUIPage,
	}
	public List<ButtonFunction> buttonFunctions;

	private Transform mainCanvasTransform;
	private CoroutineToolMethods coroutineToolMethods;
	private Button button;


	#endregion

	#region Initialization

	private void Start()
	{
		mainCanvasTransform = GameObject.Find("MainCanvas").transform;
		coroutineToolMethods = gameObject.AddComponent<CoroutineToolMethods>();
		button = gameObject.GetComponent<Button>();
		foreach (ButtonFunction function in buttonFunctions)
		{
			AddButtonFunctionality(button, function);
		}
	}

	#endregion

	#region Functionality

	private void AddButtonFunctionality(Button button, ButtonFunction buttonFunction)
	{
		switch (buttonFunction)
		{
			case ButtonFunction.ClosePanel:
				button.onClick.AddListener(delegate
				{
					foreach (GameObject panelToClose in panelsToClose)
					{
						UIToolMethods.DisableGameObject(panelToClose.name);
					}
				});
				break;
			case ButtonFunction.OpenPanel:
				button.onClick.AddListener(delegate 
				{
				foreach (GameObject panelToOpen in panelsToOpen)
				{
					UIToolMethods.OpenUIPanel(mainCanvasTransform, panelToOpen.name);
				}
				});
				break;
			case ButtonFunction.LoadScene:
				button.onClick.AddListener(delegate { StartCoroutine(coroutineToolMethods.LoadScene(sceneToOpen)); });
				break;
			case ButtonFunction.ExitGame:
				button.onClick.AddListener(delegate { UIToolMethods.ExitGame(); });
				break;
			case ButtonFunction.OpenUIPage:
				button.onClick.AddListener(delegate
				{
					UIToolMethods.OpenUIPanel(mainCanvasTransform, panelsToOpen[0].name);
					UIToolMethods.DisableGameObject(gameObject.name);
				});
				break;
		}
	}

	#endregion
}
