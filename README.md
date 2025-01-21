# AICli

A .NET Console Application that works as a console gui intermediator to MistralAi's API & Googles Gemini API.

To use, an API-Key optained from MistralAi and/or Goggle AI Studio is required.

Simply make a free account on either or both and get your key(s).

https://console.mistral.ai/
https://aistudio.google.com

Key is then inserted in user-secrets under path src/AiCli.App/ use dictionary key "ApiKeys:MistralAiApiKey" for MistralAi and "ApiKeys:AiStudioApiKey" for Google:

`dotnet user-secrets set "ApiKeys:MistralAiApiKey" "[place key here]"`

`dotnet user-secrets set "ApiKeys:AiStudioApiKey" "[place key here]"`

To run, type `dotnet run` under path src/AiCli.App/ in terminal and chat away.