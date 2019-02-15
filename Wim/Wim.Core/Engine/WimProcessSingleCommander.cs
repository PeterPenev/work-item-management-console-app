﻿using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;

namespace Wim.Core.Engine
{
    public class WimProcessSingleCommander : IWimProcessSingleCommander
    {
        public string ProcessSingleCommand(ICommand command)
        {
            switch (command.Name)
            {
                case "CreatePerson":
                    var personName = command.Parameters[0];
                    return this.CreatePerson(personName);

                case "ShowAllPeople":
                    return this.ShowAllPeople();

                case "ShowAllTeams":
                    return this.ShowAllTeams();

                case "ShowPersonsActivity":
                    var memberName = command.Parameters[0];
                    return this.ShowMemberActivityToString(memberName);

                case "CreateTeam":
                    var teamName = command.Parameters[0];
                    return this.CreateTeam(teamName);

                case "ShowTeamsActivity":
                    var teamToShowActivityFor = command.Parameters[0];
                    return this.ShowTeamActivityToString(teamToShowActivityFor);

                case "AddPersonToTeam":
                    var personToAddToTeam = command.Parameters[0];
                    var teamForAddingPersonTo = command.Parameters[1];
                    return this.AddPersonToTeam(personToAddToTeam, teamForAddingPersonTo);

                case "ShowAllTeamMembers":
                    var teamToShowMembersFor = command.Parameters[0];

                    return this.ShowAllTeamMembers(teamToShowMembersFor);

                case "CreateBoard":
                    var boardToAddToTeam = command.Parameters[0];
                    var teamForAddingBoardTo = command.Parameters[1];
                    return this.CreateBoardToTeam(boardToAddToTeam, teamForAddingBoardTo);

                case "ShowAllTeamBoards":
                    var teamToShowBoards = command.Parameters[0];
                    return this.ShowAllTeamBoards(teamToShowBoards);

                case "ShowBoardActivity":
                    var team = command.Parameters[0];
                    var boardToShowActivityFor = command.Parameters[1];
                    return this.ShowBoardActivityToString(team, boardToShowActivityFor);

                case "CreateBug":
                    var bugToAdd = command.Parameters[0];
                    var teamToAddBugFor = command.Parameters[1];
                    var boardToAddBugFor = command.Parameters[2];
                    var bugPriority = command.Parameters[3];
                    var bugSeverity = command.Parameters[4];
                    var bugAsignee = command.Parameters[5];

                    var stepsAndDescription = string.Join(' ', command.Parameters);
                    var stepsAndDescriptionArr = stepsAndDescription.Split("!Steps").ToArray();
                    var bugSteps = stepsAndDescriptionArr[1].Split('#').ToList();
                    var bugDescription = stepsAndDescriptionArr[2].ToString().Trim();

                    return this.CreateBug(bugToAdd, teamToAddBugFor, boardToAddBugFor, bugPriority, bugSeverity, bugAsignee, bugSteps, bugDescription);

                case "CreateStory":
                    var storyToAdd = command.Parameters[0];
                    var teamToAddStoryFor = command.Parameters[1];
                    var boardToAddStoryFor = command.Parameters[2];
                    var storyPriority = command.Parameters[3];
                    var storySize = command.Parameters[4];
                    var storyStatus = command.Parameters[5];
                    var storyAssignee = command.Parameters[6];

                    var buildStoryDescription = new StringBuilder();

                    for (int i = 7; i < command.Parameters.Count; i++)
                    {
                        buildStoryDescription.Append(command.Parameters[i] + " ");
                    }

                    var storyDescription = buildStoryDescription.ToString().Trim();

                    return this.CreateStory(storyToAdd, teamToAddStoryFor, boardToAddStoryFor, storyPriority, storySize, storyStatus, storyAssignee, storyDescription);

                case "CreateFeedback":
                    var feedbackToAdd = command.Parameters[0];
                    var teamToAddFeedbackFor = command.Parameters[1];
                    var boardToAddFeedbackFor = command.Parameters[2];
                    var feedbackRaiting = command.Parameters[3];
                    var feedbackStatus = command.Parameters[4];

                    var buildFeedbackDescription = new StringBuilder();

                    for (int i = 5; i < command.Parameters.Count; i++)
                    {
                        buildFeedbackDescription.Append(command.Parameters[i] + " ");
                    }

                    var feedbackDescription = buildFeedbackDescription.ToString().Trim();

                    return this.CreateFeedback(feedbackToAdd, teamToAddFeedbackFor, boardToAddFeedbackFor, feedbackRaiting, feedbackStatus, feedbackDescription);


                case "ChangeBugPriority":
                    var teamToChangeBugPriorityFor = command.Parameters[0];
                    var boardToChangeBugPriorityFor = command.Parameters[1];
                    var bugToChangePriorityFor = command.Parameters[2];
                    var newPriority = command.Parameters[3];
                    var authorOfBugPriorityChange = command.Parameters[4];

                    return this.ChangeBugPriority(teamToChangeBugPriorityFor, boardToChangeBugPriorityFor, bugToChangePriorityFor, newPriority, authorOfBugPriorityChange);

                case "ChangeBugSeverity":
                    var teamToChangeBugSeverityFor = command.Parameters[0];
                    var boardToChangeBugSeverityFor = command.Parameters[1];
                    var bugToChangeBugSeverityFor = command.Parameters[2];
                    var newSeverity = command.Parameters[3];
                    var authorOfBugSeverityChange = command.Parameters[4];

                    return this.ChangeBugSeverity(teamToChangeBugSeverityFor, boardToChangeBugSeverityFor, bugToChangeBugSeverityFor, newSeverity, authorOfBugSeverityChange);

                case "ChangeBugStatus":
                    var teamToChangeBugStatusFor = command.Parameters[0];
                    var boardToChangeBugStatusFor = command.Parameters[1];
                    var bugToChangeStatusFor = command.Parameters[2];
                    var newStatus = command.Parameters[3];
                    var authorOfBugStatusChange = command.Parameters[4];

                    return this.ChangeBugStatus(teamToChangeBugStatusFor, boardToChangeBugStatusFor, bugToChangeStatusFor, newStatus, authorOfBugStatusChange);

                case "ChangeStoryPriority":
                    var teamToChangeStoryPriorityFor = command.Parameters[0];
                    var boardToChangeStoryPriorityFor = command.Parameters[1];
                    var storyToChangePriorityFor = command.Parameters[2];
                    var newStoryPriority = command.Parameters[3];
                    var authorOfStoryPriorityChange = command.Parameters[4];

                    return this.ChangeStoryPriority(teamToChangeStoryPriorityFor, boardToChangeStoryPriorityFor, storyToChangePriorityFor, newStoryPriority, authorOfStoryPriorityChange);

                case "ChangeStorySize":
                    var teamToChangeStorySizeFor = command.Parameters[0];
                    var boardToChangeStorySizeFor = command.Parameters[1];
                    var storyToChangeSizeFor = command.Parameters[2];
                    var newStorySize = command.Parameters[3];
                    var authorOfStorySizeChange = command.Parameters[4];

                    return this.ChangeStorySize(teamToChangeStorySizeFor, boardToChangeStorySizeFor, storyToChangeSizeFor, newStorySize, authorOfStorySizeChange);

                case "ChangeStoryStatus":
                    var teamToChangeStoryStatusFor = command.Parameters[0];
                    var boardToChangeStoryStatusFor = command.Parameters[1];
                    var storyToChangeStatusFor = command.Parameters[2];
                    var newStoryStatus = command.Parameters[3];
                    var authorOfStoryStatusChange = command.Parameters[4];

                    return this.ChangeStoryStatus(teamToChangeStoryStatusFor, boardToChangeStoryStatusFor, storyToChangeStatusFor, newStoryStatus, authorOfStoryStatusChange);

                case "ChangeFeedbackRating":
                    var teamToChangeFeedbackRatingFor = command.Parameters[0];
                    var boardToChangeFeedbackRatingFor = command.Parameters[1];
                    var feedbackToChangeRatingFor = command.Parameters[2];
                    var newFeedbackRating = command.Parameters[3];
                    var authorOfFeedbackRatingChange = command.Parameters[4];

                    return this.ChangeFeedbackRating(teamToChangeFeedbackRatingFor, boardToChangeFeedbackRatingFor, feedbackToChangeRatingFor, newFeedbackRating, authorOfFeedbackRatingChange);

                case "ChangeFeedbackStatus":
                    var teamToChangeFeedbackStatusFor = command.Parameters[0];
                    var boardToChangeFeedbackStatusFor = command.Parameters[1];
                    var feedbackToChangeStatusFor = command.Parameters[2];
                    var newFeedbackStatus = command.Parameters[3];
                    var authorOfFeedbackStatusChange = command.Parameters[4];
                    return this.ChangeFeedbackStatus(teamToChangeFeedbackStatusFor, boardToChangeFeedbackStatusFor, feedbackToChangeStatusFor, newFeedbackStatus, authorOfFeedbackStatusChange);

                case "AddComment":
                    var teamToAddCommentToWorkItemFor = command.Parameters[0];
                    var boardToAddCommentToWorkItemFor = command.Parameters[1];
                    var itemTypeToAddWorkItemFor = command.Parameters[2];
                    var workitemToAddCommentFor = command.Parameters[3];
                    var authorOfComment = command.Parameters[4];

                    //build comment
                    var buildComment = new StringBuilder();

                    for (int i = 5; i < command.Parameters.Count; i++)
                    {
                        buildComment.Append(command.Parameters[i] + " ");
                    }

                    var commentToAdd = buildComment.ToString().Trim();

                    return this.AddComment(teamToAddCommentToWorkItemFor, boardToAddCommentToWorkItemFor, itemTypeToAddWorkItemFor, workitemToAddCommentFor, authorOfComment, commentToAdd);

                case "AssignUnassignItem":
                    var teamToAssignUnsignBug = command.Parameters[0];
                    var boardToAssignUnsignBug = command.Parameters[1];
                    var typeOfItem = command.Parameters[2];
                    var itemToAssignUnsign = command.Parameters[3];
                    var memberToAssignBug = command.Parameters[4];

                    return this.AssignUnassignItem(teamToAssignUnsignBug, boardToAssignUnsignBug, typeOfItem, itemToAssignUnsign, memberToAssignBug);

                case "ListAllWorkItems":
                    return this.ListAllWorkItems();

                case "FilterBugs":
                    return this.FilterBugs();

                case "FilterStories":
                    return this.FilterStories();

                case "FilterFeedbacks":
                    return this.FilterFeedbacks();

                case "FilterBugsByPriority":
                    var priorityToFilterBugFor = command.Parameters[0];
                    return this.FilterBugsByPriority(priorityToFilterBugFor);

                case "FilterBugsByAssignee":
                    var assigneeToFilterBugFor = command.Parameters[0];
                    return this.FilterBugsByAssignee(assigneeToFilterBugFor);

                case "FilterBugsByStatus":
                    var statusToFilterBugFor = command.Parameters[0];
                    return this.FilterBugsByStatus(statusToFilterBugFor);

                case "FilterStoriesByPriority":
                    var priorityToFilterStoryFor = command.Parameters[0];
                    return this.FilterStoriesByPriority(priorityToFilterStoryFor);

                case "FilterStoriesByAssignee":
                    var assigneeToFilterStoriesFor = command.Parameters[0];
                    return this.FilterStoriesByAssignee(assigneeToFilterStoriesFor);

                case "FilterStoriesByStatus":
                    var statusToFilterStoriesFor = command.Parameters[0];
                    return this.FilterStoriesByStatus(statusToFilterStoriesFor);

                case "FilterFeedbacksByStatus":
                    var statusToFilterFeedbackFor = command.Parameters[0];
                    return this.FilterFeedbacksByStatus(statusToFilterFeedbackFor);

                case "SortBugsBy":
                    var factorToSortBugBy = command.Parameters[0];
                    return this.SortBugsBy(factorToSortBugBy);

                case "SortStoriesBy":
                    var factorToSortStoriesBy = command.Parameters[0];
                    return this.SortStoriesBy(factorToSortStoriesBy);

                case "SortFeedbacksBy":
                    var factorToSortFeedbacksBy = command.Parameters[0];
                    return this.SortFeedbacksBy(factorToSortFeedbacksBy);


                default:
                    return string.Format(InvalidCommand, command.Name);
            }
        }
    }
}
