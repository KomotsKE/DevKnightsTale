using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTale.Input
{
    public class MyKeyboard
    {

        public KeyboardState newKeyboard, oldKeyboard;

        public List<Key> pressedKeys = new(), previousPressedKeys = new();

        public MyKeyboard()
        {

        }

        public virtual void Update()
        {
            newKeyboard = Microsoft.Xna.Framework.Input.Keyboard.GetState();

            GetPressedKeys();

        }

        public void UpdateOld()
        {
            oldKeyboard = newKeyboard;

            previousPressedKeys = new List<Key>();
            for (int i = 0; i < pressedKeys.Count; i++)
            {
                previousPressedKeys.Add(pressedKeys[i]);
            }
        }


        public bool GetPress(string KEY)
        {

            for (int i = 0; i < pressedKeys.Count; i++)
            {
                if (pressedKeys[i].key == KEY)
                {
                    return true;
                }
            }
            return false;
        }

        public bool GetSinglePress(string KEY)
        {
            for (int i = 0; i < pressedKeys.Count;i++)
            {
                bool pressed = false;
                for (int j = 0; j < previousPressedKeys.Count; j++)
                {
                    if (pressedKeys[i].key == previousPressedKeys[j].key)
                    {
                        pressed = true;
                        break;
                    }
                }

                if (!pressed && (pressedKeys[i].key == KEY || pressedKeys[i].print == KEY))
                {
                    return true;
                }
            }
            return false;
        }


        public virtual void GetPressedKeys()
        {
            //bool found = false;

            pressedKeys.Clear();
            for (int i = 0; i < newKeyboard.GetPressedKeys().Length; i++)
            {

                pressedKeys.Add(new Key(newKeyboard.GetPressedKeys()[i].ToString(), 1));

            }
        }
    }
}
