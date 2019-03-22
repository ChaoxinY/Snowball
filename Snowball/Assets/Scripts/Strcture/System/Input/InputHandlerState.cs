using System.Collections.Generic;

public abstract class InputHandlerState
{
    protected InputStateFactory inputStateFactory = new InputStateFactory();
    protected List<ICommand> fixedUpdateCommands = new List<ICommand>();
    public List<ICommand> FixedUpdateCommands { get { return fixedUpdateCommands; } }
	public abstract void HandleInput();
	protected InputHandlerUpdater InputHandlerUpdater { get { return InputHandlerUpdater; } set { InputHandlerUpdater = value; } }
	public void ClearUnExecutedCommandsList() { FixedUpdateCommands.Clear(); }
 }

public interface IControllerInputHandler {
    void HandleControllerInput(string inputString);
}

public interface IKeyboardMouseInputHandler
{
    void HandleKeyBoardMouseInput(string inputString);
}
