using System.Diagnostics.Contracts;

namespace DSA;

public class DynamicArray<T> 
{

    private T[] Items { get; set; }
    public int Count { get; private set; } = 0;

    private  int size;


    public DynamicArray()
    {
        size = 4;
        Items = new T[size];
        Count = 0;
    }
    public T this[int index]
    {
        get
        {
            return this.At(index);
        }
        set
        {
            this.Set(index, value);
        }
    }
    public DynamicArray(int size)
    {
        this.size = size;
        Items = new T[size];
        Count = 0;
    }



        public DynamicArray(LinkedList<T> list)
    {
        if (list == null)
            throw new ArgumentNullException(nameof(list));

        size = 4;
        Items = new T[size];
        Count = 0;

        var current = list.Head;
        while (current != null)
        {
            AddLast(current.Data);
            current = current.Next;
        }
    }


    private void Expand()
    {
        size *= 2;
        T[] temp = new T[size];
        for (int i = 0; i < Count; i++)
        {
            temp[i] = Items[i];
        }
        Items = temp;
    }

    private void Shrink()
    {
        size /= 2;
        T[] temp = new T[size];
        for (int i = 0; i < Count; i++)
        {
            temp[i] = Items[i];
        }
        Items = temp;
    }

    public void AddLast(T item)
    {
        Items[Count] = item;
        Count++;
        if (Count == size)
        {
            Expand();
        }
    }


    public void AddAt(int index, T item)
    {
        if (index < 0 || index > Count)
        {
            throw new IndexOutOfRangeException();
        }

        for (int i = Count; i > index; i--)
        {
            Items[i] = Items[i - 1];
        }

        Items[index] = item;
        Count++;
        
        if (Count == size)
        {
            Expand();
        }
    }
    
    public void RemoveAt(int index)
    {
        if (index < 0 || index >= Count)
        {
            throw new IndexOutOfRangeException();
        }

        for (int i = index; i < Count - 1; i++)
        {
            Items[i] = Items[i + 1];
        }

        Count--;
        if (Count <= size / 4)
        {
            Shrink();
        }
    }

    public void RemoveLast()
    {
        Count--;
        if (Count <= size / 4)
        {
            Shrink();
        }
    }

    public T At(int index)
    {
        if (index < 0 || index >= Count)
        {
            throw new IndexOutOfRangeException();
        }

        return Items[index];
    }

    public void Reverse()
    {
        for (int i = 0; i < Count / 2; i++)
        {
            T temp = Items[i];
            Items[i] = Items[Count - i - 1];
            Items[Count - i - 1] = temp;
        }
    }

    public void Set(int index, T item)
    {
        if (index < 0 || index >= Count)
        {
            throw new IndexOutOfRangeException();
        }

        Items[index] = item;
    }

    public T? Find(T value) {
        for (int i = 0; i < Count; i++) {
            if (Items[i]!.Equals(value)) {
                return Items[i];
            }
        }
        return default(T);
    }

    public T? Find<T1> (T1 value, Func<T, T1> key) {
        for (int i = 0; i < Count; i++) {
            if (key(Items[i]!)!.Equals(value)) {
                return Items[i];
            }
        }
        return default(T);
    }

    public bool Contains(T value) {
        for (int i = 0; i < Count; i++) {
            if (Items[i]!.Equals(value)) {
                return true;
            }
        }
        return false;
    }

    public bool ContainsAny(DynamicArray<T> values) {
        for (int i = 0; i < values.Count; i++) {
            if (Contains(values.At(i)!)) {
                return true;
            }
        }
        return false;
    }

    public bool Contains(Func<T, bool> comp) {
        for (int i = 0; i < Count; i++) {
            if (comp(Items[i]!)) {
                return true;
            }
        }
        return false;
    }

    public DynamicArray<T> Filter(Func<T, bool> key) {
        DSA.DynamicArray<T> ret = new();
        for (int i = 0; i < Count; i++) {
            if (key(Items[i]!)) {
                ret.AddLast(Items[i]!);
            }
        }
        return ret;
    }

    public override string ToString()
    {
        string ret = "";
        for (int i = 0; i < Count; i++)
        {
            ret += Items[i]!.ToString();
            if (i != Count - 1)
            {
                ret += ", ";
            }
        }
        return $"[{ret}]";
    }
    public IEnumerator<T> GetEnumerator() {
        for (int i = 0; i < Count; i++) {
            yield return Items[i];
        }
    }
}

