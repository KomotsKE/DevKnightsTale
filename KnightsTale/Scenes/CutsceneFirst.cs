using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsTale.Scenes
{
    public class CutsceneFirst : Cutscene
    {
        public CutsceneFirst()
        {
            CutsceneDialogBox = new DialogBox
            {
                Text = "                                                                                                              " +
                "'space' - следуйщая страница                              'X' - пропустить сцену \n" +
                       "I will be on the next pane! " +
                       "And wordwrap will occur, especially if there are some longer words!\n" +
                       "Monospace fonts work best but you might not want Courier New.\n" +
                       "In this code sample, after this dialog box finishes, you can press the O key to open a new one."
            };
            CutsceneDialogBox.Initialize();
        }
    }
}
