﻿using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models;
using Wim.Models.Enums;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine
{
    public class WimFactory : IWimFactory
    {      
        public ITeam CreateTeam(string name)
        {
            return new Team(name);
        }

        public IMember CreateMember(string name)
        {
            return new Member(name);
        }

        public IBoard CreateBoard(string name)
        {
            return new Board(name);
        }

        public IBug CreateBug(string title, Priority priority, Severity severity, IMember asignee, IList<string> stepsToReproduce, string description)
        {
            return new Bug(title, priority, severity, asignee, stepsToReproduce, description);
        }

        public IStory CreateStory(string title, string description, Priority priority, Size size, StoryStatus storyStatus, IMember assignee)
        {
            return new Story(title, description, priority, size, storyStatus, assignee);
        }
    }
}
