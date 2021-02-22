using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CodeGenerator
{
    public static string Generate() //random string from settings
    {
        string code = "";

        string characters = "0123456789";

        for(int i = 0; i < (int)GameSettings.Instance.gameplaySettings.codeLength; i++)
        {
            code += characters[Random.Range(0, characters.Length)];
        }

        return code;
    }

    public static string Generate(string seed) //string from seed
    {
        int hash = seed.GetHashCode();
        string code = "";

        //Random.InitState(hash);
        //int random = (Random.value.ToString()[2]);

        string hashString = hash.ToString();

        for(int i = 0; i < (int)GameSettings.Instance.gameplaySettings.codeLength; i++)
        {
            int count = i + 1;
            code += hashString[hashString.Length - count];
        }

        return code;
    }
}
