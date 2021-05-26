using CommandLine;

namespace SMT3HDSaveUtil
{
    [Verb("crypt", HelpText = "Encrypt/decrypt SMT3HD PC save data.")]
    internal class CryptOptions
    {
        [Option('i', "in", Required = true, HelpText = "Path to SMT3HD PC save file.")]
        public string PathIn { get; set; }

        [Option('o', "out", Required = false, HelpText = "Output file path.")]
        public string PathOut { get; set; }
    }
}
