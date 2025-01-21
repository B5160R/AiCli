namespace AiCli.Models;

enum RunningDistributor
{
    MistralAi,
    AiStudio,
}

enum RunningModel
{
    MistralLargeLatest,
    CodestralLatest,
    MistralModerationLatest,
}

static class RunningModelMetodes
{
    public static string GetString(this RunningModel model)
    {
        switch (model)
        {
            case RunningModel.MistralLargeLatest:
                return "mistral-large-latest";
            case RunningModel.CodestralLatest:
                return "coderal-latest";
            case RunningModel.MistralModerationLatest:
                return "mistral-moderation-latest";
        }
        return "";
    }
}
