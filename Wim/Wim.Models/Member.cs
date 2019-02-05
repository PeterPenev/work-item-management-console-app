﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Models.Interfaces;

namespace Wim.Models
{
    public class Member : IMember
    {
        private string name;

        private bool isAssigned = false;

        private List<IActivityHistory> activityHistory;

        private List<Guid> workItemsId;

        public Member(string name)
        {
            this.Name = name;
            this.activityHistory = new List<IActivityHistory>();
            this.workItemsId = new List<Guid>();
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            private set
            {
                this.name = value;
            }
        }        

        public List<Guid> WorkItemsId
        {
            get
            {
                return new List<Guid>(this.workItemsId);
            }
        }

        public List<IActivityHistory> ActivityHistory
        {
            get
            {
                return new List<IActivityHistory>(this.activityHistory);
            }
        }

        //methods

        public void AddWorkItemIdToMember(Guid workItemIdInput)
        {
            this.workItemsId.Add(workItemIdInput);
        }

        public bool FindIfMemberIsAssigned(IDictionary<string, ITeam> allTeamsInput)
        {

            foreach (var team in allTeamsInput)
            {
                if (team.Value.Members.Contains(this))
                {
                    this.isAssigned = true;
                }
                else
                {
                    this.isAssigned = false;
                }
            }

            return isAssigned;
        }

        public void AddActivityHistoryToMember(IMember memberToAddHistoryFor, IWorkItem trackedWorkItem, ITeam trackedTeam, IBoard trackedBoard)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Member: {memberToAddHistoryFor.Name} created: {trackedWorkItem.GetType().Name} with Title: {trackedWorkItem.Title} in Board: {trackedBoard.Name} part of {trackedTeam.Name} Team!");
            string resultToAddAssMessage = sb.ToString().Trim();
            var activityHistoryToAddToMember = new ActivityHistory(resultToAddAssMessage);
            activityHistory.Add(activityHistoryToAddToMember);
        }

        public string ShowMemberActivityToString(IList<IActivityHistory> activityHistoryInput)
        {
            StringBuilder sb = new StringBuilder();

            int numberOfHistories = 1;

            foreach (var history in activityHistoryInput)
            {
                var result = String.Format("{0:r}", history.LoggingDate);

                sb.AppendLine($"{numberOfHistories}. Activity with date: {result}");
                sb.AppendLine($"Activity Message:");
                sb.AppendLine($"{history.Message}");
                numberOfHistories++;
            }

            return sb.ToString().Trim();
        }
    }
}
