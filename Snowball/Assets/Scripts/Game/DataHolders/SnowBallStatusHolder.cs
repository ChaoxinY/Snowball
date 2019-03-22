using UnityEngine;

[CreateAssetMenu(fileName = "NewSnowBallStatusHolder", menuName = "Dataholders/SnowBallStatusHolder")]
public class SnowBallStatusHolder : ScriptableObject
{
    [SerializeField]
    private int snowBallPointValue;
    [SerializeField]
    private float snowBallSizeMultiplier;
	public GameObject Snowball { get { return Snowball; } set { Snowball = value; } }
	public GameObject SnowballOwner { get { return SnowballOwner; } set { SnowballOwner = value; } }
	public int LastContactedGoalID { get { return LastContactedGoalID; } set { LastContactedGoalID = value; } }

	public int SnowBallPointValue
    {
        get
        {
            return snowBallPointValue;
        }

        set
        {   
            snowBallPointValue = value;
        }
    }

    public float SnowBallSizeMultiplier
    {
        get
        {
            return snowBallSizeMultiplier;
        }

        set
        {
            snowBallSizeMultiplier = value;
            if (snowBallSizeMultiplier > 3) {
                snowBallSizeMultiplier = 3;
            }
			//Make a resizer component from this
			//Changing local scale has nothing to do with storing or accessing data
			Snowball.transform.localScale = Vector3.one * snowBallSizeMultiplier;
        }
    }
}
