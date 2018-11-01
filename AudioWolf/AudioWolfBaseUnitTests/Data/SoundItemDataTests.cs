﻿using AudioWolfStandard.Data;
using AudioWolfStandard.Services;
using AudioWolfUI.Tags;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioWolfStandardUnitTests.Data
{
    public class SoundItemDataTests
    {
        [Test]
        public void ShouldCreateTagsFromName()
        {
            TagOptions tago = new TagOptions();
            tago.Seperator = ' ';
            TagOptionsService tos = new TagOptionsService(tago);

            ServiceLocator.Instance.InjectService<TagOptionsService>(tos);

            SoundItemData sid = new SoundItemData();
            sid.Name = "Tag Tag2 Tag3";

            sid.Tags.Should().HaveCount(3);
            sid.Tags[0].Name.Should().Be("Tag");
            sid.Tags[1].Name.Should().Be("Tag2");
            sid.Tags[2].Name.Should().Be("Tag3");
        }

        [Test]
        public void ShouldCreateTagFromPipe()
        {
            TagOptions tago = new TagOptions();
            tago.Seperator = '|';
            TagOptionsService tos = new TagOptionsService(tago);
            
            ServiceLocator.Instance.InjectService<TagOptionsService>(tos); 
            SoundItemData sid = new SoundItemData();
            sid.Name = "Tag|Tag2|Tag2|TagB";

            sid.Tags.Should().HaveCount(3);
            sid.Tags[0].Name.Should().Be("Tag");
            sid.Tags[1].Name.Should().Be("Tag2");
            sid.Tags[2].Name.Should().Be("TagB");
        }
    }
}
