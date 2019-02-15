﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models;
using Wim.Models.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class SortStoriesByOperation
    {
        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;

        public SortStoriesByOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
        }

        public string SortStoriesBy(string factorToSortBy)
        {
            //Validations
            var factorTypeForChecking = $"{factorToSortBy}";
            inputValidator.IsNullOrEmpty(factorToSortBy, factorTypeForChecking);

            inputValidator.ValidateIfAnyWorkItemsExist(allTeams);

            inputValidator.ValidateIfAnyStoriesExist(allTeams);

            //Operations
            var filteredStories = new List<Story>();

            if (factorToSortBy.ToLower() == "title")
            {
                filteredStories = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Story))
                            .Select(workItem => (Story)workItem)
                                  .OrderBy(storyToOrder => storyToOrder.Title)
                                        .ToList();
            }
            else if (factorToSortBy.ToLower() == "priority")
            {
                filteredStories = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Story))
                            .Select(workItem => (Story)workItem)
                                  .OrderBy(storyToOrder => storyToOrder.Priority)
                                        .ToList();
            }
            else if (factorToSortBy.ToLower() == "status")
            {
                filteredStories = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Story))
                            .Select(workItem => (Story)workItem)
                                  .OrderBy(storyToOrder => storyToOrder.StoryStatus)
                                        .ToList();
            }
            else if (factorToSortBy.ToLower() == "size")
            {
                filteredStories = allTeams.AllTeamsList.Values
                .SelectMany(x => x.Boards)
                    .SelectMany(x => x.WorkItems)
                        .Where(x => x.GetType() == typeof(Story))
                            .Select(workItem => (Story)workItem)
                                  .OrderBy(storyToOrder => storyToOrder.Size)
                                        .ToList();
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"----ALL STORIES IN APPLICAITION SORTED BY {factorToSortBy}----");
            long workItemCounter = 1;
            foreach (var item in filteredStories)
            {
                sb.AppendLine($"{workItemCounter}. {item.GetType().Name} with name: {item.Title} ");
                workItemCounter++;
            }
            sb.AppendLine("---------------------------------");

            var resultedAllItems = sb.ToString().Trim();
            return string.Format(resultedAllItems);
        }
    }
}
