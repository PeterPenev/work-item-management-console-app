﻿using System;
using System.Collections.Generic;
using System.Text;
using Wim.Core.Contracts;
using Wim.Models.Interfaces;
using Wim.Models.Operations.Interfaces;

namespace Wim.Core.Engine.EngineOperations
{
    public class ChangeStoryStatusOperation : IEngineOperations
    {
        private const string StoryStatusChanged = "Story {0} status is changed to{1}";

        private readonly IInputValidator inputValidator;
        private readonly IAllTeams allTeams;
        private readonly IEnumParser enumParser;
        private readonly IStoryOperations storyOperations;
        private readonly IBusinessLogicValidator businessLogicValidator;

        public ChangeStoryStatusOperation(
            IInputValidator inputValidator,
            IAllTeams allTeams,
            IEnumParser enumParser,
            IStoryOperations storyOperations,
            IBusinessLogicValidator businessLogicValidator)
        {
            this.inputValidator = inputValidator;
            this.allTeams = allTeams;
            this.enumParser = enumParser;
            this.storyOperations = storyOperations;
            this.businessLogicValidator = businessLogicValidator;
        }

        public string Execute(IList<string> inputParameters)
        {
            string teamToChangeStoryStatusFor = inputParameters[0];
            string boardToChangeStoryStatusFor = inputParameters[1];
            string storyToChangeStatusFor = inputParameters[2];
            string newStoryStatus = inputParameters[3];
            string authorOfStoryStatusChange = inputParameters[4];

            //Validations
            var storyTypeForChecking = "Story Title";
            inputValidator.IsNullOrEmpty(storyToChangeStatusFor, storyTypeForChecking);

            var teamTypeForChecking = "Team Name";
            inputValidator.IsNullOrEmpty(teamToChangeStoryStatusFor, teamTypeForChecking);

            var boardTypeForChecking = "Board Name";
            inputValidator.IsNullOrEmpty(boardToChangeStoryStatusFor, boardTypeForChecking);

            var statusTypeForChecking = "Status";
            inputValidator.IsNullOrEmpty(newStoryStatus, statusTypeForChecking);

            var authorTypeForChecking = "Author";
            inputValidator.IsNullOrEmpty(authorOfStoryStatusChange, authorTypeForChecking);

            businessLogicValidator.ValidateTeamExistance(allTeams, teamToChangeStoryStatusFor);

            businessLogicValidator.ValidateBoardExistanceInTeam(allTeams, boardToChangeStoryStatusFor, teamToChangeStoryStatusFor);

            businessLogicValidator.ValidateNoSuchStoryInBoard(allTeams, boardToChangeStoryStatusFor, teamToChangeStoryStatusFor, storyToChangeStatusFor);


            //Operations
            var itemType = "Story";

            var newStatusEnum = enumParser.GetStoryStatus(newStoryStatus);

            var castedStoryForStatusChange = allTeams.FindStoryAndCast(teamToChangeStoryStatusFor, boardToChangeStoryStatusFor, storyToChangeStatusFor);

            castedStoryForStatusChange.ChangeStoryStatus(newStatusEnum);

            var memberToAddActivityFor = allTeams.FindMemberInTeam(teamToChangeStoryStatusFor, authorOfStoryStatusChange);

            var teamToAddActivityFor = allTeams.AllTeamsList[teamToChangeStoryStatusFor];

            var storyToAddActivityFor = allTeams.FindWorkItem(teamToChangeStoryStatusFor, itemType, boardToChangeStoryStatusFor, storyToChangeStatusFor);

            var teamToFindIn = allTeams.AllTeamsList[teamToChangeStoryStatusFor];

            var boardToAddActivityFor = allTeams.FindBoardInTeam(teamToChangeStoryStatusFor, boardToChangeStoryStatusFor);

            boardToAddActivityFor.AddActivityHistoryToBoard(memberToAddActivityFor, storyToAddActivityFor, newStoryStatus);

            memberToAddActivityFor.AddActivityHistoryToMember(storyToAddActivityFor, teamToFindIn, boardToAddActivityFor, newStoryStatus);

            storyOperations.AddActivityHistoryToWorkItem(storyToAddActivityFor, memberToAddActivityFor, newStoryStatus);

            return string.Format(StoryStatusChanged, storyToChangeStatusFor, newStatusEnum);

        }
    }
}
