using System.Windows.Forms;

namespace SystemTrayTools.Helpers
{
    public static class ClipboardHelpers
    {
        public static string GetTextFromClipboard()
        {
            string dataFromClipboard = null;
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                dataFromClipboard = Clipboard.GetText(TextDataFormat.Text);
            }
            return dataFromClipboard;
        }

        public static void SetTextForClipboard(string outputDataText)
        {
            Clipboard.SetText(outputDataText);
        }
    }
}
