using System.Windows.Forms;

namespace SystemTrayTools.Interfaces
{
    public interface IMenuAction
    {
        int OrderIndex { get; }

        MenuItem GetMeunItem();
    }
}