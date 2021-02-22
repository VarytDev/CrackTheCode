using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic
{
    public struct Answer
    {
        public int perfect;
        public int correct;

        public Answer(int perfect, int correct)
        {
            this.perfect = perfect;
            this.correct = correct;
        }
    }

    static public Answer CompareStrings(string code, string answer, bool campaign)
    {
        Answer result = new Answer(0, 0);

        if(campaign && code == "404")
        {
            SceneManager.LoadScene(0);
        }

        string restrictedAnswer = answer;  

        for(int i = 0; i < code.Length; i++)
        {
            if(answer[i] == code[i])
            {
                //perfect
                result.perfect++;
            }

            if(restrictedAnswer.Contains("" + code[i]))
            {
                for(int j = 0; j < restrictedAnswer.Length; j++)
                {
                    if(restrictedAnswer[j] == code[i])
                    {
                        restrictedAnswer = restrictedAnswer.Remove(j, 1);
                        //correct
                        result.correct++;
                        break;
                    }
                }
            }
        }

        result.correct -= result.perfect;

        return result;
    }
}
