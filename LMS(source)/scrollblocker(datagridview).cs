using System.Windows.Forms;

public class CustomDataGridView : DataGridView
{
    private const int WM_MOUSEWHEEL = 0x020A;
    private const int WM_VSCROLL = 0x115;
    private const int WM_HSCROLL = 0x114;

    protected override void WndProc(ref Message m)
    {
        if (m.Msg == WM_MOUSEWHEEL || m.Msg == WM_VSCROLL || m.Msg == WM_HSCROLL)
        {
            // Prevent scrolling messages
            return;
        }

        base.WndProc(ref m);
    }
}
