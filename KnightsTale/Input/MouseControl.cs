namespace KnightsTale.Input
{
    public class MouseControl
    {
        public bool Dragging, RightDrag;

        public Vector2 NewMousePos, OldMousePos, FirstMousePos, NewMouseAdjustedPos, SystemCursorPos, ScreenLoc;

        public MouseState NewMouse, OldMouse, FirstMouse;

        public MouseControl()
        {
            Dragging = false;

            NewMouse = Mouse.GetState();
            OldMouse = NewMouse;
            FirstMouse = NewMouse;

            NewMousePos = new Vector2(NewMouse.Position.X, NewMouse.Position.Y);
            OldMousePos = new Vector2(NewMouse.Position.X, NewMouse.Position.Y);
            FirstMousePos = new Vector2(NewMouse.Position.X, NewMouse.Position.Y);

            GetMouseAndAdjust();
        }

        public MouseState First
        {
            get { return FirstMouse; }
        }

        public MouseState New
        {
            get { return NewMouse; }
        }

        public MouseState Old
        {
            get { return OldMouse; }
        }

        public void Update()
        {
            GetMouseAndAdjust();

            if (NewMouse.LeftButton == ButtonState.Pressed && OldMouse.LeftButton == ButtonState.Released)
            {
                FirstMouse = NewMouse;
                FirstMousePos = NewMousePos = GetScreenPos(FirstMouse);
            }
        }

        public void UpdateOld()
        {
            OldMouse = NewMouse;
            OldMousePos = GetScreenPos(OldMouse);
        }

        public virtual float GetDistanceFromClick()
        {
            return Vector2.Distance(NewMousePos, FirstMousePos);
        }

        public virtual void GetMouseAndAdjust()
        {
            NewMouse = Mouse.GetState();
            NewMousePos = GetScreenPos(NewMouse);
        }

        public int GetMouseWheelChange()
        {
            return NewMouse.ScrollWheelValue - OldMouse.ScrollWheelValue;
        }

        public static Vector2 GetScreenPos(MouseState mouse)
        {
            Vector2 tempVec = new(mouse.Position.X, mouse.Position.Y);

            return tempVec;
        }

        public virtual bool LeftClick()
        {
            if (NewMouse.LeftButton == ButtonState.Pressed && OldMouse.LeftButton != ButtonState.Pressed && NewMouse.Position.X >= 0
                && NewMouse.Position.X <= Globals.ScreenWidth && NewMouse.Position.Y >= 0 && NewMouse.Position.Y <= Globals.ScreenHeight)
            {
                return true;
            }
            return false;
        }

        public virtual bool LeftClickHold()
        {
            bool holding = false;

            if (NewMouse.LeftButton == ButtonState.Pressed && OldMouse.LeftButton == ButtonState.Pressed && NewMouse.Position.X >= 0
                && NewMouse.Position.X <= Globals.ScreenWidth && NewMouse.Position.Y >= 0 && NewMouse.Position.Y <= Globals.ScreenHeight)
            {
                holding = true;

                if (Math.Abs(NewMouse.Position.X - FirstMouse.Position.X) > 8 || Math.Abs(NewMouse.Position.Y - FirstMouse.Position.Y) > 8)
                {
                    Dragging = true;
                }
            }
            return holding;
        }

        public virtual bool LeftClickRelease()
        {
            if (NewMouse.LeftButton == ButtonState.Released && OldMouse.LeftButton == ButtonState.Pressed)
            {
                Dragging = false;
                return true;
            }
            return false;
        }

        public virtual bool RightClick()
        {
            if (NewMouse.RightButton == ButtonState.Pressed && OldMouse.RightButton != ButtonState.Pressed && NewMouse.Position.X >= 0
                && NewMouse.Position.X <= Globals.ScreenWidth && NewMouse.Position.Y >= 0 && NewMouse.Position.Y <= Globals.ScreenHeight)
            {
                return true;
            }
            return false;
        }

        public virtual bool RightClickHold()
        {
            bool holding = false;

            if (NewMouse.RightButton == ButtonState.Pressed && OldMouse.RightButton == ButtonState.Pressed && NewMouse.Position.X >= 0
                && NewMouse.Position.X <= Globals.ScreenWidth && NewMouse.Position.Y >= 0 && NewMouse.Position.Y <= Globals.ScreenHeight)
            {
                holding = true;
                if (Math.Abs(NewMouse.Position.X - FirstMouse.Position.X) > 8 || Math.Abs(NewMouse.Position.Y - FirstMouse.Position.Y) > 8)
                {
                    RightDrag = true;
                }
            }
            return holding;
        }

        public virtual bool RightClickRelease()
        {
            if (NewMouse.RightButton == ButtonState.Released && OldMouse.RightButton == ButtonState.Pressed)
            {
                Dragging = false;
                return true;
            }
            return false;
        }
    }
}
