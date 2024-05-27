using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class missions 
{
    public string missionDescription;
        public string Description; 
        public bool IsCompleted; 
        private System.Func<User, bool> CompletionCriteria;

        public missions(string description, System.Func<User, bool> completionCriteria)
        {
            Description = description;
            CompletionCriteria = completionCriteria;
            IsCompleted = false;
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
