namespace KnightsTale.Input
{
    public class MyKeyboard
    {

        public KeyboardState NewKeyboard, OldKeyboard;

        public List<Key> PressedKeys = new(), PreviousPressedKeys = new();

        public MyKeyboard()
        {
        }

        public virtual void Update()
        {
            NewKeyboard = Keyboard.GetState();

            GetPressedKeys();

        }

        public void UpdateOld()
        {
            OldKeyboard = NewKeyboard;

            PreviousPressedKeys = new List<Key>();
            for (int i = 0; i < PressedKeys.Count; i++)
            {
                PreviousPressedKeys.Add(PressedKeys[i]);
            }
        }


        public bool GetPress(string key)
        {

            for (int i = 0; i < PressedKeys.Count; i++)
            {
                if (PressedKeys[i].key == key)
                {
                    return true;
                }
            }
            return false;
        }

        public bool GetSinglePress(string key)
        {
            for (int i = 0; i < PressedKeys.Count; i++)
            {
                bool pressed = false;
                for (int j = 0; j < PreviousPressedKeys.Count; j++)
                {
                    if (PressedKeys[i].key == PreviousPressedKeys[j].key)
                    {
                        pressed = true;
                        break;
                    }
                }

                if (!pressed && (PressedKeys[i].key == key || PressedKeys[i].Print == key))
                {
                    return true;
                }
            }
            return false;
        }


        public virtual void GetPressedKeys()
        {
            PressedKeys.Clear();
            for (int i = 0; i < NewKeyboard.GetPressedKeys().Length; i++)
            {

                PressedKeys.Add(new Key(NewKeyboard.GetPressedKeys()[i].ToString(), 1));

            }
        }
    }
}
