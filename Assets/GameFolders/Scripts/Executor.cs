using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameFolders.Scripts
{
    
    // INPUT - UPDATE - RENDER 
    public class Executor : MonoBehaviour
    {
        public static SortedDictionary<int, BaseBehaviour> behaviours = new SortedDictionary<int, BaseBehaviour>();

        private void Awake()
        {
            var array = FindObjectsOfType<BaseBehaviour>();

            foreach (var e in array)
            {
                e.Subscribe();
            }
        }
        
        //PRE-INITIALIZATION NEEDED
        private void OnEnable()
        {
            for (var i = 0; i < behaviours.Count; i++)
            {
                behaviours[i].BaseAwake();
            }
        }

        private void Start()
        {
            for (var i = 0; i < behaviours.Count; i++)
            {
                behaviours[i].BaseStart();
            }
        }

        private void Update()
        {
            for (var i = 0; i < behaviours.Count; i++)
            {
                behaviours[i].BaseUpdate();
            }
        }

        private void FixedUpdate()
        {
            for (var i = 0; i < behaviours.Count; i++)
            {
                behaviours[i].BaseFixedUpdate();
            }
        }

        private void LateUpdate()
        {
            for (var i = 0; i < behaviours.Count; i++)
            {
                behaviours[i].BaseLateUpdate();
            }
        }

        private void OnDestroy()
        {
            behaviours.Clear();
        }
    }
}
