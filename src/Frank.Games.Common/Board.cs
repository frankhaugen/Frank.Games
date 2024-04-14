namespace Frank.Games.Common;
using System;
using System.Collections.Generic;

public class Board<T> : IObservable<T>
{
    private readonly T[,] _board;
    private readonly List<IObserver<T>> _observers = new List<IObserver<T>>();

    public int Rows { get; }
    public int Columns { get; }

    public Board(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        _board = new T[rows, columns];
    }

    public T this[int row, int column]
    {
        get => _board[row, column];
        set
        {
            _board[row, column] = value;
            NotifyObservers(value);
        }
    }

    public void Initialize(Func<int, int, T> initializer)
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                this[row, column] = initializer(row, column);
            }
        }
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
            
            for (int row = 0; row < Rows; row++)
            {
                for (int column = 0; column < Columns; column++)
                {
                    observer.OnNext(this[row, column]);
                }
            }
        }
        
        return new Unsubscriber(_observers, observer);
    }

    private void NotifyObservers(T value)
    {
        foreach (var observer in _observers)
        {
            observer.OnNext(value);
        }
    }

    private class Unsubscriber : IDisposable
    {
        private readonly List<IObserver<T>> _observers;
        private readonly IObserver<T> _observer;

        public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
            {
                _observers.Remove(_observer);
            }
        }
    }
}