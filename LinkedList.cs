using System;

namespace DSA;

public class LinkedList<T>
{
    public Node<T>? Head { get; set; }
    public Node<T>? Tail { get; set; }
    public int Count { get; set; }

    public void AddLast(T val)
    {
        if (Head == null)
        {
            Head = new Node<T>(val);
            Tail = Head;
            Count = 1;
        }
        else
        {
            Node<T> temp = new Node<T>(val);
            Tail!.Next= temp;
            Tail = temp;
            Count++;
        }
    }

    public void AddFront(T val)
    {
        if (Head == null)
        {
            Head = new Node<T>(val);
            Tail = Head;
            Count = 1;
        }
        else
        {
            Node<T> temp = new(val);
            temp.Next = Head;
            Head = temp;
            Count++;
        }
    }
    public void RemoveFirst()
    {
        if(Head == null)
        {
            return;
        }
        else if(Head.Next == null)
        {
            Head = null;
            Tail = null;
            Count--;
        }
        else
        {
            Head = Head.Next;
            Count--;
        }
    }
    public void RemoveLast()
    {
        if (Head == null)
        {
            return;
        }
        else if (Head.Next == null)
        {
            Head = null;
            Tail = null;
            Count--;
        }
        else
        {
            var temp = Head;
            while(temp!.Next != Tail)
            {
                temp = temp.Next;
            }
            temp.Next = null;
            Tail = temp;
            Count--;
        }
    }

    public int Index(T val)
    {
        int i = 0;
        Node<T>? temp = Head;
        while (temp != null)
        {
            if (temp.Data.Equals(val)) { return i; }
            temp = temp.Next;
            i++;
        }
        return -1;
    }
        public void InsertAt(int index, T val)
    {
        if(index < 0 || index > Count)
        {
            return;
        }
        if (index ==0)
        {
            AddFront(val);
        }
        if(index == Count)
        {
            AddLast(val);
        }
        else
        {
            var temp = Head;
            for(int i=0; i<index-1;i++)
            {
                temp = temp!.Next;
            }
            Node<T> newNode = new(val);
            newNode.Next = temp!.Next;
            temp.Next = newNode;
            Count++;
        }
    }

    public void Remove(T val) { 
        int index = Index(val);
        if (index == -1) return;
        RemoveAt(index);
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index > Count)
        {
            return;
        }

        if (index == 0)
        {
            RemoveFirst();
        }

        else if (index == Count)
        {
            RemoveLast();
        }
        else
        {
            var temp = Head;
            for(int i=0; i<index-1; i++)
            {
                temp = temp!.Next;
            }
            temp!.Next = temp.Next!.Next;
            Count--;
        }
    }

    public bool Contains(T val)
    {
        Node<T>? temp = Head;
        while(temp != null)
        {
            if(temp.Data!.Equals(val))
            {
                return true;
            }
            temp = temp.Next;
        }
        return false;
    }

    public void Reverse()
    {
        if(Head == null)
        {
            return;
        }
        if(Head.Next == null)
        {
            return;
        }
        Node<T>? prev = null;
        Node<T>? curr = Head;
        Node<T>? next = null;
        Tail = Head;

        while(curr != null)
        {
            next = curr.Next;
            curr.Next =prev;
            prev = curr;
            curr = next;
        }
        Head = prev;
    }

    public bool Search(T val)
    {
        Node<T>? temp = Head;
        while(temp != null)
        {
            if(temp.Data!.Equals(val))
            {
                //Node newNode = new Node(temp.Data);
                //return newNode.Data;
                return true;
            }
            temp = temp.Next;
        }
        return false;
    }

    public override string ToString()
    {
        Node<T>? temp = Head;
        string ret = "";
        while (temp != null)
        {
            ret += temp.Data + "\n";
            temp = temp.Next;
        }
        return ret;
    }
    public IEnumerator<T> GetEnumerator() {
        Node<T>? temp = Head;
        while (temp != null) {
            yield return temp.Data;
            temp = temp.Next;
        }
    }
}



public class Node<T>
{
    public T Data { get; set; }
    public Node<T>? Next { get; set; }
    public Node(T data)
    {
        Data = data;
        Next = null;
    }
}



