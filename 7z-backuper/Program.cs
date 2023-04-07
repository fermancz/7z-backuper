using _7z_backuper;
using System.CommandLine;
using System.Diagnostics;
using System.Xml.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        var sourcesOption = new Option<string[]>(name: "--sources", description: "Sources directories")
        {
            AllowMultipleArgumentsPerToken = true,
            Arity = ArgumentArity.OneOrMore,
            IsRequired = true
        };
        sourcesOption.AddAlias("-s");

        var targetOption = new Option<string>(name: "--target", description: "Target directory")
        {
            Arity = ArgumentArity.ExactlyOne,
            IsRequired = true
        };
        targetOption.AddAlias("-t");

        var nameOption = new Option<string>(name: "--name", description: "Target backup name - base of file names", getDefaultValue: () => "7zbackuper")
        {
            Arity = ArgumentArity.ExactlyOne,
        };
        nameOption.AddAlias("-n");

        var z7exeOption = new Option<string>(name: "--z7exe", description: "7z/7za executable, if it isn't in path. For exampe: \"C:\\Program Files\\7za\\7za.exe\"")
        {
            Arity = ArgumentArity.ExactlyOne,
        };
        z7exeOption.AddAlias("-z");

        var diifCountOption = new Option<int>(name: "--diifCount", description: "Count of diff backups before new full backup", getDefaultValue: () => 10)
        {
            Arity = ArgumentArity.ExactlyOne,
        };
        z7exeOption.AddAlias("-d");

        var rootCommand = new RootCommand("7z Backuper command-line app");
        rootCommand.Add(sourcesOption);
        rootCommand.Add(targetOption);
        rootCommand.Add(nameOption);
        rootCommand.Add(z7exeOption);
        rootCommand.Add(diifCountOption);

        rootCommand.SetHandler((sourcesOption, targetOption, fileNameOption, z7exeOption, diifCountOption) =>
            {
                Backuper.Backup(sourcesOption, targetOption, fileNameOption, z7exeOption, diifCountOption);
            },
            sourcesOption, targetOption, nameOption, z7exeOption, diifCountOption);

        await rootCommand.InvokeAsync(args);
    }


}
