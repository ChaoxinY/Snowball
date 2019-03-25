public interface ISubject
{
	void Subscribe<T>(T item);
	void UnSubscribe<T>(T item);
}

