using AiCli.Interfaces;
using AiCli.Models;
using AiCli.Models.AiStudio;
using AiCli.Models.MistralAi;
using Microsoft.Extensions.Logging;

namespace AiCli;

public class ConsoleGui(
    ILogger<ConsoleGui> _logger,
    IMistralApiCaller _mistralApiCaller,
    IAiStudioApiCaller _aiStudioApiCaller
) : IConsoleGui
{
    private RunningDistributor _runningDistributor;
    private RunningModel _runningModel;

    public async Task Run()
    {
        _logger.LogDebug("Starting GUI...");
        GuiHeader();
        SelectAiDistributor();

        var running = true;
        while (running)
        {
            System.Console.Write("[user]: ");
            var userInput = System.Console.ReadLine();
            System.Console.WriteLine("");

            if (
                userInput == "help"
                || userInput == "q"
                || userInput == "c"
                || userInput == "ai"
                || userInput == "m"
                || userInput == "mc"
            )
            {
                switch (userInput)
                {
                    case "help":
                        System.Console.WriteLine(
                            "Type 'q' to quit \nType 'c' to change AI Distributor \nType 'ai' to see selected AI Distributor\nType 'm' to see selected model \nType 'mc' to change model"
                        );
                        break;
                    case "q":
                        running = false;
                        break;

                    case "c":
                        SelectAiDistributor();
                        break;

                    case "ai":
                        System.Console.WriteLine($"[*] {_runningDistributor}\n");
                        break;
                    case "m":
                        if (_runningDistributor != RunningDistributor.MistralAi)
                        {
                            System.Console.WriteLine("[*] Gemini 1.5 Flash\n");
                            break;
                        }
                        System.Console.WriteLine($"[*] {_runningModel}\n");
                        break;
                    case "mc":
                        if (_runningDistributor != RunningDistributor.MistralAi)
                        {
                            System.Console.WriteLine(
                                "[*] No option to change model for AiStudio implemented\n"
                            );
                            break;
                        }
                        SelectAiModel();
                        break;
                }
            }
            else if (userInput is not null)
            {
                try
                {
                    if (_runningDistributor == RunningDistributor.MistralAi)
                    {
                        var response = await GetMistralAiResponse(userInput);
                        System.Console.Write($"[mistral_ai]: {response}\n\n");
                    }
                    if (_runningDistributor == RunningDistributor.AiStudio)
                    {
                        var response = await GetAiStudioResponse(userInput);
                        System.Console.Write($"[google_ai]: {response}\n\n");
                    }
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine("[*] An error occured\n");
                    _logger.LogError(ex, "An error occured when request was made to ApiCaller");
                }
            }
        }
    }

    private async Task<string> GetMistralAiResponse(string input)
    {
        // composes request object based on user input
        var message = new Message();
        var messages = new List<Message>();
        messages.Add(message);
        message.Role = "user";
        message.Content = input;

        var request = new MistralRequest();
        request.Model = _runningModel.GetString();
        request.Messages = messages;

        // sends request to apicaller and gets response
        var response = await _mistralApiCaller.GetRequest(request);

        // retruns the ai generated response from response object
        return response?.Choices?.FirstOrDefault()?.Message?.Content
            ?? throw new ArgumentNullException("Content from response object was null");
    }

    private async Task<string> GetAiStudioResponse(string input)
    {
        // compose request object based on user input
        var request = new AiStudioRequest
        {
            Contents = new List<Content>
            {
                new Content { Parts = new List<Part> { new Part { Text = input } } },
            },
        };

        var response = await _aiStudioApiCaller.GetRequest(request);

        return response.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text
            ?? throw new ArgumentNullException("Content from response object was null");
    }

    private void GuiHeader()
    {
        System.Console.WriteLine("");
        System.Console.WriteLine(
            @" █████  ██      ██████ ██      ██ 
██   ██ ██     ██      ██      ██ 
███████ ██     ██      ██      ██ 
██   ██ ██     ██      ██      ██ 
██   ██ ██      ██████ ███████ ██ 
        ");
        System.Console.WriteLine("AI_CLI - a terminal connection to AI APIs (MistralAI & GoogleAI");
        System.Console.WriteLine("Type 'help' to see commands\n");
    }

    // Sets which AI to use and thus sets which ApiCaller to make the reques:t to
    private void SelectAiDistributor()
    {
        var inputInProgress = true;

        while (inputInProgress)
        {
            System.Console.WriteLine("Select which AI to use:\n 1) MistralAi\n 2) AiStudio");
            var userInput = System.Console.ReadLine();
            if (userInput == "1" || userInput == "2")
            {
                switch (userInput)
                {
                    case "1":
                        _runningDistributor = RunningDistributor.MistralAi;
                        goto default;
                    case "2":
                        _runningDistributor = RunningDistributor.AiStudio;
                        goto default;
                    default:
                        inputInProgress = false;
                        break;
                }
                System.Console.WriteLine($"[*] {_runningDistributor} selected\n");
            }
            else
            {
                System.Console.WriteLine("[*] Input is  invalid. Try again\n");
            }
        }
    }

    // Sets which Model to use
    private void SelectAiModel()
    {
        var inputInProgress = true;

        while (inputInProgress)
        {
            System.Console.WriteLine(
                "Select which model to use:\n 1) MistralLargeLatest\n 2) CodestralLatest\n 3) MistralModerationLatest\n"
            );
            var userInput = System.Console.ReadLine();
            if (userInput == "1" || userInput == "2" || userInput == "3")
            {
                switch (userInput)
                {
                    case "1":
                        _runningModel = RunningModel.MistralLargeLatest;
                        goto default;
                    case "2":
                        _runningModel = RunningModel.CodestralLatest;
                        goto default;
                    case "3":
                        _runningModel = RunningModel.MistralModerationLatest;
                        goto default;
                    default:
                        inputInProgress = false;
                        break;
                }
                System.Console.WriteLine($"[*] {_runningModel} selected\n");
            }
            else
            {
                System.Console.WriteLine("[*] Input is not invalid. Try again\n");
            }
        }
    }
}
