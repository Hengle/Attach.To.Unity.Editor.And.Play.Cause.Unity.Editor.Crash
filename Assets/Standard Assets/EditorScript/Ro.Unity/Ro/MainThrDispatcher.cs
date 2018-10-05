#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;

namespace EditorScript.Ro
{
    [InitializeOnLoad]
    public class MainThrDispatcher
    {
        static MainThrDispatcher()
        {
            EditorApplication.update += DispatchTask;
        }

        private static Queue<Action> tasks = new Queue<Action>();

        public void AddTask(Action action)
        {
            if (Thread.CurrentThread == mainThr)
            {
                action();
                return;
            }

            lock (tasks)
            {
                tasks.Enqueue(action);
            }
        }

        private static Thread mainThr;

        public static void DispatchTask()
        {
            mainThr = Thread.CurrentThread;
            while (true)
            {
                if (tasks.Count == 0)
                {
                    return;
                }
                else
                {
                    var act = tasks.Dequeue();
                    act();
                }
            }
        }
    }
}
#endif