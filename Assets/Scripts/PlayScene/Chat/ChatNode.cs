using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChatNode : MonoBehaviour
{
    public abstract void GetCommand(string func, string parameter);
    public abstract void GetCommand(string func);
}
