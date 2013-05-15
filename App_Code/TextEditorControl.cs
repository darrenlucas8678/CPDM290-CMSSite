using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AjaxControlToolkit.HTMLEditor;

namespace CPDM.LucasD.Controls
{
    /// <summary>
    /// Summary description for TextEditorControl
    /// </summary>
    public class TextEditorControl : Editor
    {

        protected override void FillTopToolbar()
        {
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.Bold());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.Italic());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.Underline());
        }

        protected override void FillBottomToolbar()
        {
           
        }
    }
}
