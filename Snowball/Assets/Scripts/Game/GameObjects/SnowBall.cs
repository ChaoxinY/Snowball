using UnityEngine;

public class SnowBall : MonoBehaviour, ISnowBallStatusHolder
{
	[SerializeField]
	private EventSubject eventSubject;
	private SnowBallCollisionHandler snowBallCollisionHandler;
	public SnowBallStatusHolder snowBallStatusHolder;

	private void Start()
	{
		snowBallCollisionHandler = new SnowBallCollisionHandler(gameObject, eventSubject);
		snowBallStatusHolder.Snowball = gameObject;
	}

	private void OnCollisionEnter(Collision collision)
	{
		snowBallCollisionHandler.ReactToCollision(collision);
	}

	public SnowBallStatusHolder GetSnowBallStatusHolder()
	{
		return snowBallStatusHolder;
	}

	public void SetSnowBallStatusHolder(SnowBallStatusHolder snowBallStatusHolder)
	{
		this.snowBallStatusHolder = snowBallStatusHolder;
	}
}
