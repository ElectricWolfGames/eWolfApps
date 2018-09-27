using System;
using System.Windows.Forms;
using SystemTrayTools.Interfaces;

namespace SystemTrayTools.Actions
{
    public class PlayBackMessageFromClipBoard : IMenuAction
    {
        public int OrderIndex
        {
            get
            {
                return 10;
            }
        }

        public MenuItem GetMeunItem()
        {
            return new MenuItem("PlayBackMessage", new EventHandler(PlayBackMessage));
        }

        private void PlayBackMessage(object sender, EventArgs e)
        {
            IDataObject dataObject = Clipboard.GetDataObject();
            if (dataObject.GetDataPresent(DataFormats.Text))
            {
                string str = (string)dataObject.GetData(DataFormats.Text);
                System.Speech.Synthesis.SpeechSynthesizer sp = new System.Speech.Synthesis.SpeechSynthesizer();
                sp.Speak(str);
            }
        }
    }
}
