using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace eWolfPodcasterUI.Groups
{
    public class TreeGroupHolder
    {
        private const char _divider = '|';
        private readonly Dictionary<string, TreeGroupDetails> _groups = new Dictionary<string, TreeGroupDetails>();
        private TreeView _rootNode;

        public TreeGroupHolder(TreeView showsItemsTree)
        {
            _rootNode = showsItemsTree;
        }

        public void Add(string groupName)
        {
            string[] parts = groupName.Split(_divider);
            if (parts.Length == 1)
            {
                CreateRootNode(parts[0]);
            }

            string path = string.Empty;
            foreach (var part in parts)
            {
                path += part;
                var groupDetails = GetOrMake(path);
                path += _divider;
            }
        }

        private TreeGroupDetails CreateNode(string parent, string groupName)
        {
            TreeGroupDetails tdg = null;
            if (!_groups.TryGetValue(parent, out tdg))
            {
                return null;
            }

            TreeViewItem groupNode = new TreeViewItem
            {
                Header = groupName
            };

            tdg.GroupNode.Items.Add(groupNode);

            TreeGroupDetails groupDetails = new TreeGroupDetails
            {
                GroupName = groupName,
                GroupPath = parent + _divider + groupName,
                GroupNode = groupNode
            };

            _groups.Add(groupDetails.GroupPath, groupDetails);
            return groupDetails;
        }

        private TreeGroupDetails CreateRootNode(string groupName)
        {
            TreeViewItem groupNode = new TreeViewItem
            {
                Header = groupName
            };

            _rootNode.Items.Add(groupNode);

            TreeGroupDetails groupDetails = new TreeGroupDetails
            {
                GroupName = groupName,
                GroupPath = groupName,
                GroupNode = groupNode
            };

            _groups.Add(groupDetails.GroupPath, groupDetails);
            return groupDetails;
        }

        private TreeGroupDetails GetOrMake(string groupPath)
        {
            TreeGroupDetails node = null;
            TreeGroupDetails tdg = null;
            if (_groups.TryGetValue(groupPath, out tdg))
            {
                return tdg;
            }

            string[] parts = groupPath.Split(_divider);
            if (parts.Length == 1)
            {
                return CreateRootNode(parts[0]);
            }

            string name = parts[parts.Length - 1];

            var allParts = parts.ToList();
            string parent = string.Empty;
            for (int i = 0; i < allParts.Count - 1; i++)
            {
                if (!string.IsNullOrWhiteSpace(parent))
                    parent += _divider;
                parent += allParts[i];
            }
            node = CreateNode(parent, name);

            return node;
        }
    }
}