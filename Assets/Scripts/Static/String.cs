using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class String
{
    public static string AddLine(string oldString, string newLine)
    {
        oldString += "\n" + newLine;
        return oldString;
    }

    public static string Numbers(int number)
    {
        if(number == 1) return " number ";
        else return " numbers ";
    }

    public static string Space()
    {
        return "\n \n" + "===============================" + "\n \n";
    }
}
