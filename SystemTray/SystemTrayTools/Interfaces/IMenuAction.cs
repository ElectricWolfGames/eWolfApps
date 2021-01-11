using System.Windows.Forms;

namespace SystemTrayTools.Interfaces
{
    public interface IMenuAction
    {
        MenuItem GetMeunItem();

        int OrderIndex { get; }
    }
}