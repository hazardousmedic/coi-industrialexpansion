using System;
using System.Collections;
using System.Collections.Generic;

internal sealed class ArrayListHelper<T> : IEnumerable, ICollection, IList, IEnumerable<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, ICollection<T>, IList<T>
{
    public ArrayListHelper(T[] items)
    {
        this._items = items;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this._items.GetEnumerator();
    }

    int ICollection.Count
    {
        get
        {
            return this._items.Length;
        }
    }

    bool ICollection.IsSynchronized
    {
        get
        {
            return false;
        }
    }

    object ICollection.SyncRoot
    {
        get
        {
            return this;
        }
    }

    void ICollection.CopyTo(Array array, int index)
    {
        this._items.CopyTo(array, index);
    }

    object IList.this[int index]
    {
        get
        {
            return this._items[index];
        }
        set
        {
            throw new NotSupportedException();
        }
    }

    bool IList.IsFixedSize
    {
        get
        {
            return true;
        }
    }

    bool IList.IsReadOnly
    {
        get
        {
            return true;
        }
    }

    int IList.Add(object value)
    {
        throw new NotSupportedException();
    }

    void IList.Clear()
    {
        throw new NotSupportedException();
    }

    bool IList.Contains(object value)
    {
        return Array.IndexOf(this._items, value) >= 0;
    }

    int IList.IndexOf(object value)
    {
        return Array.IndexOf(this._items, value);
    }

    void IList.Insert(int index, object value)
    {
        throw new NotSupportedException();
    }

    void IList.Remove(object value)
    {
        throw new NotSupportedException();
    }

    void IList.RemoveAt(int index)
    {
        throw new NotSupportedException();
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return ((IEnumerable<T>)this._items).GetEnumerator();
    }

    int IReadOnlyCollection<T>.Count
    {
        get
        {
            return this._items.Length;
        }
    }

    T IReadOnlyList<T>.this[int index]
    {
        get
        {
            return this._items[index];
        }
    }

    int ICollection<T>.Count
    {
        get
        {
            return this._items.Length;
        }
    }

    bool ICollection<T>.IsReadOnly
    {
        get
        {
            return true;
        }
    }

    void ICollection<T>.Add(T item)
    {
        throw new NotSupportedException();
    }

    void ICollection<T>.Clear()
    {
        throw new NotSupportedException();
    }

    bool ICollection<T>.Contains(T item)
    {
        return Array.IndexOf(this._items, item) >= 0;
    }

    void ICollection<T>.CopyTo(T[] array, int arrayIndex)
    {
        this._items.CopyTo(array, arrayIndex);
    }

    bool ICollection<T>.Remove(T item)
    {
        throw new NotSupportedException();
    }

    T IList<T>.this[int index]
    {
        get
        {
            return this._items[index];
        }
        set
        {
            throw new NotSupportedException();
        }
    }

    int IList<T>.IndexOf(T item)
    {
        return Array.IndexOf(this._items, item);
    }

    void IList<T>.Insert(int index, T item)
    {
        throw new NotSupportedException();
    }

    void IList<T>.RemoveAt(int index)
    {
        throw new NotSupportedException();
    }

    private readonly T[] _items;
}