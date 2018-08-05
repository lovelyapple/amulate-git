using System;
public class SingleTonBase<T> where T : class, new()
{
    private static T _instance = null;
    public T Get()
    {
        if(_instance == null)
        {
            _instance = new T();
        }

        return _instance;
    }

}
