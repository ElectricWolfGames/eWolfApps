using eWolfPodcasterCore.Data;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;

[assembly: InternalsVisibleTo("eWolfPodcasterCoreUnitTests")]

namespace eWolfPodcasterCore.Helpers
{
    public static class RSSHelper
    {
        internal static List<EpisodeControl> ReadEpisodes(XmlReader reader)
        {
            List<EpisodeControl> episodes = new List<EpisodeControl>();
            EpisodeControl showData = new EpisodeControl();

            string elementName = string.Empty;

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        elementName = reader.Name;

                        if (elementName == "enclosure")
                        {
                            string attr = reader.GetAttribute(0);
                            showData.PodcastURL = attr;
                            if (!attr.Contains("mp3"))
                                showData.PodcastURL = reader.GetAttribute(2);
                        }

                        break;

                    case XmlNodeType.CDATA:
                        break;

                    case XmlNodeType.Text:
                        showData.SetTextNodeData(elementName, reader.Value);
                        break;

                    case XmlNodeType.EndElement:
                        if (reader.Name == "item")
                        {
                            episodes.Add(showData);
                            showData = new EpisodeControl();
                        }
                        break;
                }
            }
            return episodes;
        }
    }
}
