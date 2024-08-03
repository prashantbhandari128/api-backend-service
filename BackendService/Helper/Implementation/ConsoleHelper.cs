using BackendService.Helper.Interface;

namespace BackendService.Helper.Implementation
{
    public class ConsoleHelper : IConsoleHelper
    {
        // +-------------------------------------------------+
        // |    Format : ESC[{attr1};{attr2};....{attrn}m    |
        // +-------------------------------------------------+

        private const string ESCAPE = "\u001b[{0}m";
        private const string ENDC = "\u001b[0m";

        // ---------------[ Text Formatting ]--------------
        public const string REGULAR = "0";
        public const string BOLD = "1";
        public const string LOW_INTENSITY = "2"; // Not widely supported
        public const string ITALIC = "3";
        public const string UNDERLINE = "4";
        public const string BLINKING = "5";
        public const string REVERSE = "6"; // Not widely supported
        public const string BACKGROUND = "7";
        public const string INVISIBLE = "8";
        // ------------------------------------------------

        // -----------------[ Text Colors ]----------------
        public static readonly Dictionary<string, string> COLORS = new Dictionary<string, string>
        {
            { "black", "30" },
            { "red", "31" },
            { "green", "32" },
            { "yellow", "33" },
            { "blue", "34" },
            { "magenta", "35" },
            { "cyan", "36" },
            { "white", "37" }
        };
        // ------------------------------------------------

        // -------------[ Background Colors ]--------------
        public static readonly Dictionary<string, string> BACKGROUND_COLORS = new Dictionary<string, string>
        {
            { "black", "40" },
            { "red", "41" },
            { "green", "42" },
            { "yellow", "43" },
            { "blue", "44" },
            { "magenta", "45" },
            { "cyan", "46" },
            { "white", "47" }
        };
        // ------------------------------------------------

        public string Decorate(string textFormattingOption, string bgColor, string fgColor, string message)
        {
            /*
            ======================================================================
            Apply the specified formatting to the message using ANSI escape codes.
            ======================================================================

            Args:
                textFormattingOption (string): Text formatting attribute.
                bgColor (string): Background color.
                fgColor (string): Foreground color.
                message (string): The message to be formatted.

            Returns:
                string: The formatted message.
            */

            List<string> formatAttributes = new List<string>(){
                textFormattingOption,
                bgColor,
                fgColor
            };
            string formatString = string.Join(";", formatAttributes);
            string formatSequence = string.Format(ESCAPE, formatString);

            return $"{formatSequence}{message}{ENDC}";
        }
    }
}
