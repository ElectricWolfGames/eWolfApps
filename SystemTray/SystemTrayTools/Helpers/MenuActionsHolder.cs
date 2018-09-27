using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using SystemTrayTools.Interfaces;

namespace SystemTrayTools.Helpers
{
    public sealed class MenuActionsHolder
    {
        private List<IMenuAction> _menuActions;

        public ContextMenu GetMenu(Form parent)
        {
            ContextMenu contextMenu = new ContextMenu();

            foreach (var instance in _menuActions)
            {
                IHaveAccessToMainForm needFormSetup = instance as IHaveAccessToMainForm;
                needFormSetup?.SetUp(parent);

                contextMenu.MenuItems.Add(instance.GetMeunItem());
            }
            return contextMenu;
        }

        public void PopulateList()
        {
            var instancced = from t in Assembly.GetExecutingAssembly().GetTypes()
                             where t.GetInterfaces().Contains(typeof(IMenuAction))
                                   && t.GetConstructor(Type.EmptyTypes) != null
                             select Activator.CreateInstance(t) as IMenuAction;

            _menuActions = new List<IMenuAction>();
            _menuActions.AddRange(instancced.ToList());
        }

        public void UpdateClipboard(string clipboardText)
        {
            foreach (IMenuAction isel in _menuActions)
            {
                IUpdateWithClipBoard iu = isel as IUpdateWithClipBoard;
                if (iu != null)
                {
                    iu.UpdateFromClipBoard(clipboardText);
                }
            }
        }
    }
}
