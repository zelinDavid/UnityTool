using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T: new()
{
    static public T ShareInstance{get;} = new T();
    
}
