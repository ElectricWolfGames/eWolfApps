using System.Windows.Forms;

namespace SystemTrayTools.Interfaces
{
    internal interface IHaveAccessToMainForm
    {
        void SetUp(Form mainForm);
    }
}