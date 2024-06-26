﻿using KnightsTale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTale.Managers
{
    public class SceneManager
    {
        public Stack<IScene> sceneStack;

        public SceneManager()
        {
            sceneStack = new Stack<IScene>();
        }

        public void AddScene(IScene scene) { scene.Load(); sceneStack.Push(scene); }

        public void RemoveScene() { sceneStack.Pop(); }

        public IScene GetCurrentScene() { return sceneStack.Peek(); }
    }
}
